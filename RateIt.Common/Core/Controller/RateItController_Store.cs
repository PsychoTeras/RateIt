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

#region Private methods

        private void AssertStoreInfo(Store store)
        {
            //Check user on null-reference
            if (store == null)
            {
                throw BaseQueryResult.Throw("Store is null-reference",
                    ECGeneric.NullReference);
            }

            //Validate store name
            store.StoreName = (store.StoreName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(store.StoreName))
            {
                throw BaseQueryResult.Throw("Store name cannot be blank",
                    ECStoreRegistration.StoreNameIsBlank);
            }

            //Validate minimal store name length
            if (store.StoreName.Length < GenericConstants.STORE_NAME_LENGTH_MIN)
            {
                string errMsg = string.Format("Store name should have {0} letters at least",
                                              GenericConstants.STORE_NAME_LENGTH_MIN);
                throw BaseQueryResult.Throw(errMsg,
                    ECStoreRegistration.MinStoreNameLengthRequired);
            }

            //Validate maximal store name length
            if (store.StoreName.Length > GenericConstants.STORE_NAME_LENGTH_MAX)
            {
                string errMsg = string.Format("Store name should not have more than {0} letters",
                                              GenericConstants.STORE_NAME_LENGTH_MAX);
                throw BaseQueryResult.Throw(errMsg,
                    ECStoreRegistration.MaxStoreNameLengthExceeded);
            }

            //Validate maximal address length
            store.Address = (store.Address ?? string.Empty).Trim();
            store.Address2 = (store.Address2 ?? string.Empty).Trim();
            if (store.Address.Length > GenericConstants.STORE_ADDRESS_LENGTH_MAX ||
                store.Address2.Length > GenericConstants.STORE_ADDRESS_LENGTH_MAX)
            {
                string errMsg = string.Format("Store address should not have more than {0} letters",
                                              GenericConstants.STORE_ADDRESS_LENGTH_MAX);
                throw BaseQueryResult.Throw(errMsg,
                    ECStoreRegistration.MaxAddressLengthExceeded);
            }

            //Validate maximal description length
            store.Description = (store.Description ?? string.Empty).Trim();
            if (store.Description.Length > GenericConstants.STORE_DESCRIPTION_LENGTH_MAX)
            {
                string errMsg = string.Format("Store description should not have more than {0} letters",
                                              GenericConstants.STORE_DESCRIPTION_LENGTH_MAX);
                throw BaseQueryResult.Throw(errMsg,
                    ECStoreRegistration.MaxStoreNameLengthExceeded);
            }

            //Validate geocoordinates and store size
            if (store.Location == null || !store.Location.IsValid ||
                store.Size == null || !store.Size.IsValid)
            {
                throw BaseQueryResult.Throw("Store location is invalid",
                    ECStoreRegistration.InvalidGeoCoordinates);
            }
        }

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

#endregion

#region Public methods

        public BaseQueryResult StoreRegister(UserSessionInfo sessionInfo, Store store)
        {
            try
            {
                //Assert session information
                AssertSessionInfo(sessionInfo);

                //Assert registration information
                AssertStoreInfo(store);

                //Register store
                try
                {
                    _storeDAL.StoreRegister(store);

                    //Add log record
                    AddActionLogRecord(ActionLogType.Store_Register, sessionInfo.UserId);
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
