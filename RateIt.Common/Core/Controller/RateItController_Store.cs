using System;
using RateIt.Common.Core.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryParams;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class RateItController
    {

#region Constants

        private const byte MIN_STORE_NAME_LENGTH = 3 ;
        private const byte MAX_STORE_NAME_LENGTH = 50;

#endregion

#region Private methods

        private void AssertGetStoresAtLocation(GeoPoint location)
        {
            //Check location on null-reference
            if (location == null)
            {
                throw BaseQueryResult.Throw("Location is null-reference",
                                            ECGeneric.NullReference);
            }
        }

        private void AssertGetStoresAtLocation(QuerySysRequestID sysId, GeoRectangle rectangle)
        {
            //Check system request id
            if (sysId != QuerySysRequestID.Instance)
            {
                throw BaseQueryResult.Throw("Invalid system request id",
                    ECGeneric.InvalidSysRequestId);
            }

            //Check rectangle on null-reference
            if (rectangle == null)
            {
                throw BaseQueryResult.Throw("Rectangle is null-reference",
                    ECGeneric.NullReference);
            }
        }

        private void AssertRegistrationInfo(Store registrationInfo)
        {
            //Check registrationInfo on null-reference
            if (registrationInfo == null)
            {
                throw BaseQueryResult.Throw("Registration info is null-reference", 
                    ECGeneric.NullReference);
            }

            //Validate store name
            registrationInfo.StoreName = (registrationInfo.StoreName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(registrationInfo.StoreName))
            {
                throw BaseQueryResult.Throw("Store name cannot be blank",
                    ECStoreRegistration.NameIsBlank);
            }

            //Validate minimal store name length
            if (registrationInfo.StoreName.Length < MIN_STORE_NAME_LENGTH)
            {
                string errMsg = string.Format("Store name should have {0} letters at least", 
                                              MIN_STORE_NAME_LENGTH);
                throw BaseQueryResult.Throw(errMsg, 
                    ECStoreRegistration.MinNameLengthRequired);
            }

            //Validate maximal store name length
            if (registrationInfo.StoreName.Length > MAX_STORE_NAME_LENGTH)
            {
                string errMsg = string.Format("Store name should not have more than {0} letters", 
                                              MAX_STORE_NAME_LENGTH);
                throw BaseQueryResult.Throw(errMsg, 
                    ECStoreRegistration.MaxNameLengthExceeded);
            }
        }

#endregion

#region Public methods

        public BaseQueryResult StoreRegister(UserSessionInfo sessionInfo, Store registrationInfo)
        {
            try
            {
                //Assert session information
                AssertSessionInfo(sessionInfo);

                //Assert registration information
                AssertRegistrationInfo(registrationInfo);

                //Register store
                try
                {
                    _storeDAL.StoreRegister(registrationInfo);

                    //Add log record
                    //AddActionLogRecord(ActionLogType.Store_Register, sessionInfo.UserId);
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<BaseQueryResult>(dbEx, ECGeneric.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<BaseQueryResult>(ex);
            }

            //Done
            return BaseQueryResult.Successful;
        }

        public StoreListQueryResult GetStoresAtLocation(UserSessionInfo sessionInfo, 
            GeoPoint location,  StoreQueryAreaLevel areaLevel)
        {
            try
            {
                //Assert session information
                AssertSessionInfo(sessionInfo);

                //Assert location information
                AssertGetStoresAtLocation(location);

                //Get stores at location
                try
                {
                    Store[] stores = _storeDAL.GetStoresAtLocation(location, areaLevel);
                    return new StoreListQueryResult(stores);
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<StoreListQueryResult>(dbEx, ECGeneric.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<StoreListQueryResult>(ex);
            }
        }

#endregion

#region System methods

        public StoreListQueryResult GetStoresAtLocationSys(QuerySysRequestID sysId, GeoRectangle rectangle)
        {
            try
            {
                //Assert location information
                AssertGetStoresAtLocation(sysId, rectangle);

                //Get stores at location
                try
                {
                    Store[] stores = _storeDAL.GetStoresAtLocation(rectangle);
                    return new StoreListQueryResult(stores);
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<StoreListQueryResult>(dbEx, ECGeneric.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<StoreListQueryResult>(ex);
            }
        }

#endregion

    }
}
