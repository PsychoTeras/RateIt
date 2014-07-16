using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Web
{
     [ServiceContract]
    public interface IRateItAndroid
    {

#region User actions

//        [OperationContract]
//        BaseQueryResult      UserRegister(string tId, User user);
//        [OperationContract]
//        UserLoginQueryResult UserLogin(UserLoginInfo loginInfo);
//        [OperationContract]
//        BaseQueryResult      UserLogout(UserSessionInfo sessionInfo);

        [OperationContract]
        [WebGet(UriTemplate = "/UserLoginA/?userName={userName}&?passwordHash={passwordHash}&?tid={tid}", ResponseFormat = WebMessageFormat.Json)]
        UserLoginQueryResult UserLoginA(string userName, string passwordHash, string tid);
#endregion

#region Store actions

//        [OperationContract]
//        BaseQueryResult      StoreRegister(UserSessionInfo sessionInfo, Store user);
//        [OperationContract]
//        StoreListQueryResult GetStoresAtLocation(UserSessionInfo sessionInfo, GeoPoint location, 
//                                                 StoreQueryAreaLevel areaLevel);

#endregion

#region Product actions

//        [OperationContract]
//        ProductRegisterQueryResult ProductRegister(UserSessionInfo sessionInfo, Product product);

#endregion

    }
}