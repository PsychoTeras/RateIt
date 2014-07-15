using RateIt.Common.Core.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Controller;
using RateIt.Common.Core.Entities.Products;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Web
{
    public class RateItFacade : IRateItController
    {

#region Private fields

        private readonly RateItController _controller;

#endregion

#region Class methods

        public RateItFacade()
        {
            _controller = new RateItController();
        }

#endregion

#region IRateItController methods

        public BaseQueryResult UserRegister(string tId, User user)
        {
            return _controller.UserRegister(tId, user);
        }

        public UserLoginQueryResult UserLogin(UserLoginInfo loginInfo)
        {
            return _controller.UserLogin(loginInfo);
        }

        public BaseQueryResult UserLogout(UserSessionInfo sessionInfo)
        {
            return _controller.UserLogout(sessionInfo);
        }

        public BaseQueryResult StoreRegister(UserSessionInfo sessionInfo, Store store)
        {
            return _controller.StoreRegister(sessionInfo, store);
        }

        public StoreListQueryResult GetStoresAtLocation(UserSessionInfo sessionInfo, GeoPoint location,
                                                        StoreQueryAreaLevel areaLevel)
        {
            return _controller.GetStoresAtLocation(sessionInfo, location, areaLevel);
        }

        public ProductRegisterQueryResult ProductRegister(UserSessionInfo sessionInfo, Product product)
        {
            return _controller.ProductRegister(sessionInfo, product);
        }

        public ProductListQueryResult GetProducts(UserSessionInfo sessionInfo, byte[] storeId, 
                                                  string productName)
        {
            return _controller.GetProducts(sessionInfo, storeId, productName);
        }

#endregion

    }
}
