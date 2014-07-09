using System.ServiceModel;
using MongoDB.Bson;
using RateIt.Common.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Common.Core.Controller
{
    [ServiceContract]
    public interface IRateItController
    {

#region User actions

        [OperationContract]
        BaseQueryResult UserRegister(User registrationInfo);
        [OperationContract]
        UserQueryResult UserLogin(UserLoginInfo loginInfo);
        [OperationContract]
        BaseQueryResult UserLogout(ObjectId userId);
        [OperationContract]
        UserListQueryResult GetUserList(string userNamePart, int maxCount);

#endregion

#region Store action

        [OperationContract]
        BaseQueryResult StoreRegister(Store registrationInfo);
        [OperationContract]
        StoreListQueryResult GetStoresAtLocation(GeoPoint location, StoreQueryAreaLevel areaLevel);

#endregion

    }
}
