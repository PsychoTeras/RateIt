﻿using System;
using System.Windows.Forms;
using RateIt.Common.Core.Controller;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryResults;
using RateIt.Common.Helpers;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    public partial class frmUserRegister : Form
    {

#region Private members

        private IRateItController _controller;

#endregion

#region DoRegister method

        public static bool DoRegister(IRateItController controller)
        {
            using (frmUserRegister form = new frmUserRegister())
            {
                form._controller = controller;
                return form.ShowDialog() == DialogResult.OK;
            }
        }

#endregion

#region Class methods

        private frmUserRegister()
        {
            InitializeComponent();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            string passwordHash = CommonHelper.GetHashSum(tbPassword.Text);
            User user = new User(tbUserName.Text, passwordHash, tbEmail.Text);
            BaseQueryResult result = _controller.UserRegister(UserLoginInfo.DEFAULT_TID, user);
            if (!Helper.CheckOnValidQueryResult(result))
            {
                switch (result.ErrorCode)
                {
                    case ECUserRegistration.UserNameIsBlank:
                    case ECUserRegistration.MinUserNameLengthRequired:
                    case ECUserRegistration.MaxUserNameLengthExceeded:
                    case ECUserRegistration.InvalidUserName:
                    case ECUserRegistration.UserExists:
                        tbUserName.Focus();
                        break;
                    case ECUserRegistration.PasswordIsBlank:
                    case ECUserRegistration.InvalidPasswordHash:
                        tbPassword.Focus();
                        break;
                    case ECUserRegistration.InvalidEmail:
                        tbEmail.Focus();
                        break;
                }
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void FrmUserRegisterKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        BtnOkClick(btnOk, null);
                        break;
                    }
                case Keys.Escape:
                    {
                        Close();
                        break;
                    }
            }
        }

#endregion

    }
}
