using System;
using RateIt.Common.Classes;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class MainController
    {

#region Private methods

        private void AssertGetStoresAtLocation(GeoPoint location)
        {
            //Check on null-reference
            if (location == null)
            {
                throw BaseQueryResult.Throw("Location is null-reference",
                                            ECGeneral.NullReference);
            }
        }

        private void AssertRegistrationInfo(Store registrationInfo)
        {
            //Check on null-reference
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

        public StoreListQueryResult GetStoresAtLocation(GeoPoint location)
        {
            try
            {
                Store[] storesList = null;
                return new StoreListQueryResult(storesList);
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<StoreListQueryResult>(ex);
            }
        }

#endregion

    }
}
