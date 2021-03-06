﻿using System;
using System.Windows.Forms;
using RateIt.Common.Core.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Controller;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryResults;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    public partial class frmStoreRegister : Form
    {

#region Private members

        private IRateItController _controller;
        private UserSessionInfo _sessionInfo;
        private GeoPoint _atLocation;
        private Store _store;

#endregion

#region DoRegister method

        public static Store DoRegister(IRateItController controller, UserSessionInfo sessionInfo,
                                       GeoPoint atLocation)
        {
            using (frmStoreRegister form = new frmStoreRegister())
            {
                form._controller = controller;
                form._sessionInfo = sessionInfo;
                form._atLocation = atLocation;
                return form.ShowDialog() == DialogResult.OK ? form._store : null;
            }
        }

#endregion

#region Class methods

        public frmStoreRegister()
        {
            InitializeComponent();
            cbSize.SelectedIndex = 1;
        }

        private GeoSize GetSelectedStoreSizeInMeters()
        {
            ushort size = 0;
            switch (cbSize.SelectedIndex)
            {
                    //Small
                case 0:
                {
                    size = (ushort) StoreSize.Small;
                    break;
                }
                    //Medium
                case 1:
                {
                    size = (ushort) StoreSize.Medium;
                    break;
                }
                    //Large
                case 2:
                {
                    size = (ushort) StoreSize.Large;
                    break;
                }
                    //Huge
                case 3:
                {
                    size = (ushort) StoreSize.Huge;
                    break;
                }
            }
            return new GeoSize(size, size);
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            GeoSize storeSize = GetSelectedStoreSizeInMeters();
            Store store = new Store(tbName.Text, tbAddress.Text, tbDescription.Text,
                                    _atLocation, storeSize);
            BaseQueryResult result = _controller.StoreRegister(_sessionInfo, store);
            if (!Helper.CheckOnValidQueryResult(result))
            {
                switch (result.ErrorCode)
                {
                    case ECStoreRegistration.StoreNameIsBlank:
                    case ECStoreRegistration.MinStoreNameLengthRequired:
                    case ECStoreRegistration.MaxStoreNameLengthExceeded:
                        tbName.Focus();
                        break;
                    case ECStoreRegistration.MaxAddressLengthExceeded:
                        tbAddress.Focus();
                        break;
                    case ECStoreRegistration.MaxDescriptionLengthExceeded:
                        tbDescription.Focus();
                        break;
                }
                return;
            }
            _store = store;
            DialogResult = DialogResult.OK;
        }

        private void FrmShopRegisterKeyDown(object sender, KeyEventArgs e)
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
