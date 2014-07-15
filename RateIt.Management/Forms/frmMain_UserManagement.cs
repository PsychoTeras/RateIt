using System;
using System.Linq;
using System.Windows.Forms;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.QueryParams;
using RateIt.Common.Core.QueryResults;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    partial class frmMain
    {

#region Private members

        private User _loggedUser;
        private UserSessionInfo _loggedUserSession;

#endregion

#region Properties

        private User SelectedUser
        {
            get
            {
                return lvUsers.SelectedItems.Count > 0
                           ? (User)lvUsers.SelectedItems[0].Tag
                           : null;
            }
        }

        private User LoggedUser
        {
            get { return _loggedUser; }
        }

#endregion

#region User management methods

        public void InitializeUserManagement()
        {
            LvUsersSelectedIndexChanged(lvUsers, null);
            Helper.UserSessionExpired += UserSessionExpired;
            RefreshUserList();
        }


        private void UnloginCurrentUser(bool isSessionExpired)
        {
            User loggedUser = LoggedUser;
            if (loggedUser != null)
            {
                loggedUser.IsUserLogged = false;
                _loggedUser = null;
                _loggedUserSession = null;
                AfterCurrentUserHasLoggedOut(loggedUser.UserName, isSessionExpired);
            }
        }

        private void UserSessionExpired()
        {
            UnloginCurrentUser(true);
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
                User user = (User)item.Tag;
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

            foreach (User user in result.UserList)
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

        private void AfterSelectedUserHasLoggedIn(string userName)
        {
            gbUserLogged.Enabled = true;
            tbLoggedUserName.Text = userName;
            WriteLog(string.Format("User {0} successfully logged in", _loggedUser));
            UpdateSelectedUserListRecord();
        }

        private void AfterSelectedUserHasLoggedOut()
        {
            WriteLog(string.Format("User {0} successfully logged out",
                     SelectedUser.UserName));
            UpdateSelectedUserListRecord();
        }

        private void AfterCurrentUserHasLoggedOut(string userName, bool isSessionExpired)
        {
            gbUserLogged.Enabled = false;
            tbLoggedUserName.Text = string.Empty;
            WriteLog(!isSessionExpired
                ? string.Format("User {0} successfully logged out", userName)
                : string.Format("Session for user {0} has expired", userName));
            UpdateUserListRecord(lvUsers.Items.Cast<ListViewItem>().FirstOrDefault
                (
                    i => ((User)i.Tag).UserName == userName)
                );
        }

        private void BtnCurrentUserLogoutClick(object sender, EventArgs e)
        {
            if (btnCurrentUserLogout.Enabled && _loggedUserSession != null)
            {
                BaseQueryResult result = _mainController.UserLogout(_loggedUserSession);
                if (Helper.CheckOnValidQueryResult(result))
                {
                    UnloginCurrentUser(false);
                }
            }
        }

#endregion

    }
}
