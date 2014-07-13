using System;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.DAL;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryParams;
using RateIt.Common.Core.QueryResults;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class RateItController : IRateItController, IRateItControllerSys, IDisposable
    {

#region Private members

        private UserDAL _userDAL = new UserDAL();
        private UserSessionDAL _userSessionDAL = new UserSessionDAL();
        private StoreDAL _storeDAL = new StoreDAL();
        private ActionLogDAL _actionLogDAL = new ActionLogDAL();
        private ProductDAL _productDAL = new ProductDAL();

#endregion

#region Private methods

        private void AssertQuerySysRequestID(QuerySysRequestID sysId)
        {
            //Check system request id
            if (sysId != QuerySysRequestID.Instance)
            {
                throw BaseQueryResult.Throw("Invalid system request id",
                    ECGeneric.InvalidSysRequestId);
            }
        }

        private void AssertSessionInfo(UserSessionInfo sessionInfo)
        {
            AssertSessionInfo(sessionInfo, false);
        }

        private void AssertSessionInfo(UserSessionInfo sessionInfo, bool assertOnly)
        {
            //Validate session info
            if (sessionInfo == null)
            {
                throw BaseQueryResult.Throw("Session info is null-reference. Do login before continue",
                    ECGeneric.InvalidSessionInfo);
            }

            //Check session ID
            if (sessionInfo.UserId == null ||
                sessionInfo.UserId.Length != GenericConstants.OBJECT_ID_LENGTH)
            {
                throw BaseQueryResult.Throw("Invalid user ID",
                    ECGeneric.InvalidSessionInfo);
            }

            //Check session ID
            if (sessionInfo.SessionId == null || 
                sessionInfo.SessionId.Length != GenericConstants.OBJECT_ID_LENGTH)
            {
                throw BaseQueryResult.Throw("Invalid session ID",
                    ECGeneric.InvalidSessionInfo);
            }

            //Validate session
            if (!_userSessionDAL.UpdateUserSession(sessionInfo, assertOnly))
            {
                throw BaseQueryResult.Throw("User session is expired or invalid. Please re-login",
                    ECGeneric.InvalidSessionInfo);
            }
        }

#endregion

#region Class methods

        public void Dispose()
        {
            InternalHelper.SafeDispose(ref _userDAL);
            InternalHelper.SafeDispose(ref _userSessionDAL);
            InternalHelper.SafeDispose(ref _storeDAL);
            InternalHelper.SafeDispose(ref _actionLogDAL);
            InternalHelper.SafeDispose(ref _productDAL);
        }

#endregion

    }
}
