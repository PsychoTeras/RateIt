using MongoDB.Bson;
using RateIt.Common.Classes;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Common.Core.Controller
{
    public interface IController
    {

#region User actions

        BaseQueryResult     UserRegister(User registrationInfo);
        UserQueryResult     UserLogin(UserLoginInfo loginInfo);
        BaseQueryResult     UserLogout(ObjectId userId);
        UserListQueryResult GetUserList(string userNamePart, int maxCount);

#endregion

#region Store action

        BaseQueryResult StoreRegister(Store registrationInfo);
        StoreListQueryResult GetStoresAtLocation(GeoPoint location);


#endregion

    }
}
