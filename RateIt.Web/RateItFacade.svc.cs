using MongoDB.Bson;
using RateIt.Common.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Controller;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Web
{
    public class RateItFacade : IRateItController
    {
        public BaseQueryResult UserRegister(User registrationInfo)
        {
            throw new System.NotImplementedException();
        }

        public UserQueryResult UserLogin(UserLoginInfo loginInfo)
        {
            throw new System.NotImplementedException();
        }

        public UserListQueryResult GetUserList(string userNamePart, int maxCount)
        {
            throw new System.NotImplementedException();
        }

        public BaseQueryResult StoreRegister(Store registrationInfo)
        {
            throw new System.NotImplementedException();
        }

        public StoreListQueryResult GetStoresAtLocation(GeoPoint location, StoreQueryAreaLevel areaLevel)
        {
            throw new System.NotImplementedException();
        }

        public BaseQueryResult UserLogout(ObjectId userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
