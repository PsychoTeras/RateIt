using System;
using System.Windows.Forms;
using RateIt.Common.Core.Controller;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryResults;
using RateIt.Common.Helpers;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    public partial class frmUserLogin : Form
    {

#region Private members

        private IRateItController _controller;

#endregion

#region Properties

        public byte[] UserId { get; private set; }
        public byte[] SessionId { get; private set; }

#endregion

#region DoLogin method

        public bool Execute(IRateItController controller, User user)
        {
            _controller = controller;
            tbUserName.Text = user.UserName;
            return ShowDialog() == DialogResult.OK;
        }

#endregion

#region Class methods

        public frmUserLogin()
        {
            InitializeComponent();
            tbPassword.Select();
        }

        private void BtnOkClick(object sender, System.EventArgs e)
        {
            string passwordHash = CommonHelper.GetHashSum(tbPassword.Text);
            UserLoginInfo loginInfo = new UserLoginInfo(tbUserName.Text, passwordHash);

            UserLoginQueryResult result = _controller.UserLogin(loginInfo);
            if (!Helper.CheckOnValidQueryResult(result))
            {
                switch (result.ErrorCode)
                {
                    case ECLogin.UserNameIsBlank:
                    case ECLogin.MinUserNameLengthRequired:
                    case ECLogin.MaxUserNameLengthExceeded:
                        {
                            tbUserName.Focus();
                            break;
                        }
                    case ECLogin.PasswordIsBlank:
                    case ECLogin.InvalidPasswordHash:
                    case ECLogin.InvalidCrenedtials:
                        {
                            tbPassword.Focus();
                            break;
                        }
                }
                return;
            }

            UserId = Convert.FromBase64String(result.UserId);
            SessionId = Convert.FromBase64String(result.SessionId);
            DialogResult = DialogResult.OK;
        }

        private void FrmUserLoginKeyDown(object sender, KeyEventArgs e)
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
