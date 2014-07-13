using System.ServiceModel;
using RateIt.Common.Core.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.Products;
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
        BaseQueryResult      UserRegister(string tId, User user);
        [OperationContract]
        UserLoginQueryResult UserLogin(UserLoginInfo loginInfo);
        [OperationContract]
        BaseQueryResult      UserLogout(UserSessionInfo sessionInfo);

#endregion

#region Store actions

        [OperationContract]
        BaseQueryResult      StoreRegister(UserSessionInfo sessionInfo, Store user);
        [OperationContract]
        StoreListQueryResult GetStoresAtLocation(UserSessionInfo sessionInfo, GeoPoint location, 
                                                 StoreQueryAreaLevel areaLevel);

#endregion

#region Product actions

        [OperationContract]
        ProductRegisterQueryResult ProductRegister(UserSessionInfo sessionInfo, Product product);

#endregion

    }
}
