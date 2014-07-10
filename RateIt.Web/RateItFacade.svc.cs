using RateIt.Common.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Controller;
using RateIt.Common.Core.Entities.Session;
using RateIt.Common.Core.Entities.Sessions;
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

        public BaseQueryResult UserRegister(User registrationInfo)
        {
            return _controller.UserRegister(registrationInfo);
        }

        public UserLoginQueryResult UserLogin(UserLoginInfo loginInfo)
        {
            return _controller.UserLogin(loginInfo);
        }

        public BaseQueryResult StoreRegister(UserSessionInfo sessionInfo, Store registrationInfo)
        {
            return _controller.StoreRegister(sessionInfo, registrationInfo);
        }

        public StoreListQueryResult GetStoresAtLocation(UserSessionInfo sessionInfo, GeoPoint location, 
                                                        StoreQueryAreaLevel areaLevel)
        {
            return _controller.GetStoresAtLocation(sessionInfo, location, areaLevel);
        }

        public BaseQueryResult UserLogout(UserSessionInfo sessionInfo)
        {
            return _controller.UserLogout(sessionInfo);
        }

#endregion

    }
}
