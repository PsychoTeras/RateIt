using System;
using System.Linq;
using System.Windows.Forms;
using RateIt.Common.Core.Entities.Session;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.QueryParams;
using RateIt.Common.Core.QueryResults;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    partial class frmMain
    {

#region Private members

        private SessionInfo _loggedUserSession;

#endregion

#region Properties

        private UserListItem SelectedUserListItem
        {
            get
            {
                return lvUsers.SelectedItems.Count > 0
                           ? (UserListItem) lvUsers.SelectedItems[0].Tag
                           : null;
            }
        }

        private UserListItem LoggedUserListItem
        {
            get
            {
                if (_loggedUserSession != null)
                {
                    ListViewItem item = lvUsers.Items.Cast<ListViewItem>().FirstOrDefault
                        (
                            li => ((UserListItem) li.Tag).UserName.Equals
                                      (
                                          _loggedUserSession.UserName, StringComparison.InvariantCultureIgnoreCase
                                      )
                        );
                    return (UserListItem) (item != null ? item.Tag : null);
                }
                return null;
            }
        }

#endregion

#region User management methods

        public void InitializeUserManagement()
        {
            LvUsersSelectedIndexChanged(lvUsers, null);
            RefreshUserList();
        }

        private void UpdateSelectedUserListRecord()
        {
            if (lvUsers.SelectedItems.Count > 0)
            {
                UpdateUserListRecord(lvUsers.SelectedItems[0]);
            }
        }

        private void UpdateUserListRecord(ListViewItem item)
        {
            if (item != null)
            {
                UserListItem user = (UserListItem)item.Tag;
                item.SubItems[0].Text = user.UserName;
                item.SubItems[1].Text = user.Email;
                item.SubItems[2].Text = user.IsUserLogged.ToText();
            }
            LvUsersSelectedIndexChanged(lvUsers, null);
        }

        private void RefreshUserList()
        {
            //Get list of users
            UserListQueryResult result = _mainControllerSys.GetUserListSys(
                QuerySysRequestID.Instance, tbUserSearch.Text.Trim(), 
                (uint) ntbUsersCount.Value);

            //Something failed
            if (!Helper.CheckOnValidQueryResult(result))
            {
                return;
            }

            //Otherwise, populate list of users
            lvUsers.BeginUpdate();
            lvUsers.Items.Clear();

            foreach (UserListItem user in result.UserList)
            {
                ListViewItem item = new ListViewItem(user.UserName);
                item.SubItems.Add(user.Email);
                item.SubItems.Add(user.IsUserLogged.ToText());
                item.Tag = user;
                lvUsers.Items.Add(item);
            }

            gbUserList.Text = string.Format("User list [{0}]", result.UserList.Length);

            lvUsers.EndUpdate();
        }

        private void TbUserSearchTextChanged(object sender, EventArgs e)
        {
            RefreshUserList();
        }

        private void BtnUserRegisterClick(object sender, EventArgs e)
        {
            if (frmUserRegister.DoRegister(_mainController))
            {
                RefreshUserList();
            }
        }

        private void LvUsersSelectedIndexChanged(object sender, EventArgs e)
        {
            btnUserLogin.Enabled = SelectedUserListItem != null && 
                                   _loggedUserSession == null;
            btnUserLogout.Enabled = SelectedUserListItem != null &&
                                    SelectedUserListItem != LoggedUserListItem &&
                                    SelectedUserListItem.IsUserLogged;
        }

        private void LvUsersMouseDoubleClick(object sender, MouseEventArgs e)
        {
            BtnUserLoginClick(this, null);
        }

        private void AfterSelectedUserHasLoggedIn()
        {
            gbUserLogged.Enabled = true;
            tbLoggedUserName.Text = _loggedUserSession.UserName;
            WriteLog(string.Format("User {0} successfully logged in", _loggedUserSession));
            UpdateSelectedUserListRecord();
        }

        private void BtnUserLoginClick(object sender, EventArgs e)
        {
            if (btnUserLogin.Enabled && SelectedUserListItem != null)
            {
                using (frmUserLogin form = new frmUserLogin())
                {
                    if (form.Execute(_mainController, SelectedUserListItem))
                    {
                        _loggedUserSession = new SessionInfo(form.UserName, form.SessionId);
                        SelectedUserListItem.IsUserLogged = true;
                        AfterSelectedUserHasLoggedIn();
                    }
                }
            }
        }

        private void AfterSelectedUserHasLoggedOut()
        {
            WriteLog(string.Format("User {0} successfully logged out",
                     SelectedUserListItem.UserName));
            UpdateSelectedUserListRecord();
        }

        private void BtnUserLogoutClick(object sender, EventArgs e)
        {
            if (btnUserLogout.Enabled && SelectedUserListItem != null)
            {
                BaseQueryResult result = _mainControllerSys.UserLogoutSys(
                    QuerySysRequestID.Instance, SelectedUserListItem.UserName);
                if (Helper.CheckOnValidQueryResult(result))
                {
                    SelectedUserListItem.IsUserLogged = false;
                    AfterSelectedUserHasLoggedOut();
                }
            }
        }

        private void AfterCurrentUserHasLoggedOut(string userName)
        {
            gbUserLogged.Enabled = false;
            tbLoggedUserName.Text = string.Empty;
            WriteLog(string.Format("User {0} successfully logged out", userName));
            UpdateUserListRecord(lvUsers.Items.Cast<ListViewItem>().FirstOrDefault
                (
                    i => ((UserListItem) i.Tag).UserName == userName)
                );
        }

        private void BtnCurrentUserLogoutClick(object sender, EventArgs e)
        {
            if (btnCurrentUserLogout.Enabled && _loggedUserSession != null)
            {
                BaseQueryResult result = _mainController.UserLogout(_loggedUserSession);
                if (Helper.CheckOnValidQueryResult(result))
                {
                    UserListItem loggedUserListItem = LoggedUserListItem;
                    if (loggedUserListItem != null)
                    {
                        loggedUserListItem.IsUserLogged = false;
                        _loggedUserSession = null;
                        AfterCurrentUserHasLoggedOut(loggedUserListItem.UserName);
                    }
                }
            }
        }

#endregion


    }
}
