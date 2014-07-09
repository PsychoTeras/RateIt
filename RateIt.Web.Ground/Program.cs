using System.Linq;
using RateIt.Common.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.Session;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.QueryResults;
using RateIt.Common.Helpers;
using RateIt.Web.Ground.RateItWebService;

namespace RateIt.Web.Ground
{
    class Program
    {

        private static readonly RateItControllerClient _service = new RateItControllerClient();

        static void Main(string[] args)
        {
            string passwordHash = CommonHelper.GetHashSum("1");
            UserLoginInfo loginInfo = new UserLoginInfo("1", passwordHash);
            UserLoginQueryResult loginResult = _service.UserLogin(loginInfo);
            if (!loginResult.HasError)
            {
                SessionInfo sessionInfo = new SessionInfo(loginResult.UserName, loginResult.SessionId);
                StoreListQueryResult storesQueryResult = _service.GetStoresAtLocation(sessionInfo, 
                    new GeoPoint(49.9511370682993, 36.2615132331848), StoreQueryAreaLevel.Level3);

                _service.UserLogout(sessionInfo);

                if (!storesQueryResult.HasError)
                {
                    Store firstStore = storesQueryResult.Stores.First();
                }
                _service.UserLogout(sessionInfo);
            }
        }
    }
}
