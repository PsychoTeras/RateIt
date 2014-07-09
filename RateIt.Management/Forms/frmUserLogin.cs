using System.Windows.Forms;
using RateIt.Common.Core.Controller;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryResults;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    public partial class frmUserLogin : Form
    {

#region Private members

        private IRateItController _controller;

#endregion

#region DoLogin method

        public static bool DoLogin(IRateItController controller, User user)
        {
            using (frmUserLogin form = new frmUserLogin())
            {
                form._controller = controller;
                form.tbUserName.Text = user.UserName;
                return form.ShowDialog() == DialogResult.OK;
            }
        }

#endregion

#region Class methods

        private frmUserLogin()
        {
            InitializeComponent();
            tbPassword.Select();
        }

        private void BtnOkClick(object sender, System.EventArgs e)
        {
            UserLoginInfo loginInfo = new UserLoginInfo(tbUserName.Text, tbPassword.Text);
            UserQueryResult result = _controller.UserLogin(loginInfo);
            if (!Helper.CheckOnValidQueryResult(result))
            {
                switch (result.ErrorCode)
                {
                    case ECLogin.UserNameIsBlank:
                        tbUserName.Focus();
                        break;
                    case ECLogin.PasswordIsBlank:
                    case ECLogin.InvalidCrenedtials:
                        tbPassword.Focus();
                        break;
                }
                return;
            }
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
