using System;
using RateIt.Common.Core.DAL;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class MainController : IController, IDisposable
    {

#region Private members

        private UserDAL _userDAL = new UserDAL();
        private UserLoginDAL _userLoginDAL = new UserLoginDAL();
        private StoreDAL _storeDAL = new StoreDAL();

#endregion

#region Class methods

        public void Dispose()
        {
            CommonHelper.SafeDispose(ref _userDAL);
            CommonHelper.SafeDispose(ref _userLoginDAL);
            CommonHelper.SafeDispose(ref _storeDAL);
        }

#endregion

    }
}
