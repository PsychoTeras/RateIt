using System.ServiceModel;
using RateIt.Common.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.Session;
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
        BaseQueryResult      UserRegister(User registrationInfo);
        [OperationContract]
        UserLoginQueryResult UserLogin(UserLoginInfo loginInfo);
        [OperationContract]
        BaseQueryResult      UserLogout(SessionInfo sessionInfo);

#endregion

#region Store action

        [OperationContract]
        BaseQueryResult      StoreRegister(SessionInfo sessionInfo, Store registrationInfo);
        [OperationContract]
        StoreListQueryResult GetStoresAtLocation(SessionInfo sessionInfo, GeoPoint location, 
                                                 StoreQueryAreaLevel areaLevel);

#endregion

    }
}
