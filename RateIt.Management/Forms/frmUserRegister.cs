using System;
using System.Windows.Forms;
using RateIt.Common.Core.Controller;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryResults;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    public partial class frmUserRegister : Form
    {

#region Private members

        private IController _controller;

#endregion

#region DoRegister method

        public static bool DoRegister(IController controller)
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
            User user = new User(tbUserName.Text, tbPassword.Text, tbEmail.Text);
            BaseQueryResult result = _controller.UserRegister(user);
            if (!Helper.CheckOnValidQueryResult(result))
            {
                switch (result.ErrorCode)
                {
                    case ECUserRegistration.UserNameIsBlank:
                        tbUserName.Focus();
                        break;
                    case ECUserRegistration.PasswordIsBlank:
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
