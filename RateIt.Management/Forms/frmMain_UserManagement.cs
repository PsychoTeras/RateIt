using System;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Bson;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.QueryResults;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    partial class frmMain
    {

#region Private members

        private UserListItem _loggedUser;

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

#endregion

#region User management methods

        public void InitializeUserManagement()
        {
            _loggedUser = null;
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
            UserListQueryResult result = _mainController.GetUserList(
                tbUserSearch.Text.Trim(), (int) ntbUsersCount.Value);

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
                                   _loggedUser == null;
            btnUserLogout.Enabled = SelectedUserListItem != null &&
                                    SelectedUserListItem != _loggedUser &&
                                    SelectedUserListItem.IsUserLogged;
        }

        private void LvUsersMouseDoubleClick(object sender, MouseEventArgs e)
        {
            BtnUserLoginClick(this, null);
        }

        private void AfterSelectedUserHasLoggedIn()
        {
            gbUserLogged.Enabled = true;
            tbLoggedUserName.Text = _loggedUser.UserName;
            WriteLog(string.Format("User {0} successfully logged in", _loggedUser));
            UpdateSelectedUserListRecord();
        }

        private void BtnUserLoginClick(object sender, EventArgs e)
        {
            if (btnUserLogin.Enabled && SelectedUserListItem != null)
            {
                if (frmUserLogin.DoLogin(_mainController, SelectedUserListItem))
                {
                    _loggedUser = SelectedUserListItem;
                    _loggedUser.IsUserLogged = true;
                    AfterSelectedUserHasLoggedIn();
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
                BaseQueryResult result = _mainController.UserLogout(SelectedUserListItem.Id);
                if (Helper.CheckOnValidQueryResult(result))
                {
                    SelectedUserListItem.IsUserLogged = false;
                    AfterSelectedUserHasLoggedOut();
                }
            }
        }

        private void AfterCurrentUserHasLoggedOut(string userName, ObjectId userId)
        {
            gbUserLogged.Enabled = false;
            tbLoggedUserName.Text = string.Empty;
            WriteLog(string.Format("User {0} successfully logged out", userName));
            UpdateUserListRecord(lvUsers.Items.Cast<ListViewItem>().FirstOrDefault
                                     (i => ((UserListItem) i.Tag).Id == userId));
        }

        private void BtnCurrentUserLogoutClick(object sender, EventArgs e)
        {
            if (btnCurrentUserLogout.Enabled && _loggedUser != null)
            {
                BaseQueryResult result = _mainController.UserLogout(_loggedUser.Id);
                if (Helper.CheckOnValidQueryResult(result))
                {
                    _loggedUser.IsUserLogged = false;
                    string userName = _loggedUser.UserName;
                    ObjectId userId = _loggedUser.Id;
                    _loggedUser = null;
                    AfterCurrentUserHasLoggedOut(userName, userId);
                }
            }
        }

#endregion


    }
}
