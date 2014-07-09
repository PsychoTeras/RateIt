using System;
using RateIt.Common.Core.DAL;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class RateItController : IRateItController, IRateItControllerSys, IDisposable
    {

#region Private members

        private UserDAL _userDAL = new UserDAL();
        private UserSessionDAL _userSessionDAL = new UserSessionDAL();
        private StoreDAL _storeDAL = new StoreDAL();

#endregion

#region Class methods

        public void Dispose()
        {
            InternalHelper.SafeDispose(ref _userDAL);
            InternalHelper.SafeDispose(ref _userSessionDAL);
            InternalHelper.SafeDispose(ref _storeDAL);
        }

#endregion

    }
}
