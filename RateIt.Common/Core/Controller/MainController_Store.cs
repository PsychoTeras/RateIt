using System;
using RateIt.Common.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryParams;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class MainController
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
                                            ECGeneral.NullReference);
            }
        }

        private void AssertGetStoresAtLocation(QuerySysRequestID sysId, GeoRectangle rectangle)
        {
            //Check system request id
            if (sysId != QuerySysRequestID.Instance)
            {
                throw BaseQueryResult.Throw("Invalid system request id",
                    ECGeneral.InvalidSysRequestId);
            }

            //Check rectangle on null-reference
            if (rectangle == null)
            {
                throw BaseQueryResult.Throw("Rectangle is null-reference",
                    ECGeneral.NullReference);
            }
        }

        private void AssertRegistrationInfo(Store registrationInfo)
        {
            //Check registrationInfo on null-reference
            if (registrationInfo == null)
            {
                throw BaseQueryResult.Throw("Registration info is null-reference", 
                    ECGeneral.NullReference);
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

        public BaseQueryResult StoreRegister(Store registrationInfo)
        {
            try
            {
                //Assert registration information
                AssertRegistrationInfo(registrationInfo);

                //Register store
                try
                {
                    _storeDAL.StoreRegister(registrationInfo);
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<BaseQueryResult>(dbEx, ECGeneral.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<BaseQueryResult>(ex);
            }

            //Done
            return BaseQueryResult.Successful;
        }

        public StoreListQueryResult GetStoresAtLocation(GeoPoint location, StoreQueryAreaLevel areaLevel)
        {
            try
            {
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
                    return BaseQueryResult.FromException<StoreListQueryResult>(dbEx, ECGeneral.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<StoreListQueryResult>(ex);
            }
        }

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
                    return BaseQueryResult.FromException<StoreListQueryResult>(dbEx, ECGeneral.DBError);
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
