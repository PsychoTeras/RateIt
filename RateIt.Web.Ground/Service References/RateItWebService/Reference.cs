﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RateIt.Web.Ground.RateItWebService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RateItWebService.IRateItController")]
    public interface IRateItController {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateItController/UserRegister", ReplyAction="http://tempuri.org/IRateItController/UserRegisterResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RateIt.Common.Core.QueryResults.UserLoginQueryResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RateIt.Common.Core.QueryResults.StoreListQueryResult))]
        RateIt.Common.Core.QueryResults.BaseQueryResult UserRegister(string tId, RateIt.Common.Core.Entities.Users.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateItController/UserLogin", ReplyAction="http://tempuri.org/IRateItController/UserLoginResponse")]
        RateIt.Common.Core.QueryResults.UserLoginQueryResult UserLogin(RateIt.Common.Core.Entities.Users.UserLoginInfo loginInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateItController/UserLogout", ReplyAction="http://tempuri.org/IRateItController/UserLogoutResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RateIt.Common.Core.QueryResults.UserLoginQueryResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RateIt.Common.Core.QueryResults.StoreListQueryResult))]
        RateIt.Common.Core.QueryResults.BaseQueryResult UserLogout(RateIt.Common.Core.Entities.Users.UserSessionInfo sessionInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateItController/StoreRegister", ReplyAction="http://tempuri.org/IRateItController/StoreRegisterResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RateIt.Common.Core.QueryResults.UserLoginQueryResult))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RateIt.Common.Core.QueryResults.StoreListQueryResult))]
        RateIt.Common.Core.QueryResults.BaseQueryResult StoreRegister(RateIt.Common.Core.Entities.Users.UserSessionInfo sessionInfo, RateIt.Common.Core.Entities.Stores.Store user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRateItController/GetStoresAtLocation", ReplyAction="http://tempuri.org/IRateItController/GetStoresAtLocationResponse")]
        RateIt.Common.Core.QueryResults.StoreListQueryResult GetStoresAtLocation(RateIt.Common.Core.Entities.Users.UserSessionInfo sessionInfo, RateIt.Common.Core.Classes.GeoPoint location, RateIt.Common.Core.Constants.StoreQueryAreaLevel areaLevel);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRateItControllerChannel : RateIt.Web.Ground.RateItWebService.IRateItController, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RateItControllerClient : System.ServiceModel.ClientBase<RateIt.Web.Ground.RateItWebService.IRateItController>, RateIt.Web.Ground.RateItWebService.IRateItController {
        
        public RateItControllerClient() {
        }
        
        public RateItControllerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RateItControllerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RateItControllerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RateItControllerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public RateIt.Common.Core.QueryResults.BaseQueryResult UserRegister(string tId, RateIt.Common.Core.Entities.Users.User user) {
            return base.Channel.UserRegister(tId, user);
        }
        
        public RateIt.Common.Core.QueryResults.UserLoginQueryResult UserLogin(RateIt.Common.Core.Entities.Users.UserLoginInfo loginInfo) {
            return base.Channel.UserLogin(loginInfo);
        }
        
        public RateIt.Common.Core.QueryResults.BaseQueryResult UserLogout(RateIt.Common.Core.Entities.Users.UserSessionInfo sessionInfo) {
            return base.Channel.UserLogout(sessionInfo);
        }
        
        public RateIt.Common.Core.QueryResults.BaseQueryResult StoreRegister(RateIt.Common.Core.Entities.Users.UserSessionInfo sessionInfo, RateIt.Common.Core.Entities.Stores.Store user) {
            return base.Channel.StoreRegister(sessionInfo, user);
        }
        
        public RateIt.Common.Core.QueryResults.StoreListQueryResult GetStoresAtLocation(RateIt.Common.Core.Entities.Users.UserSessionInfo sessionInfo, RateIt.Common.Core.Classes.GeoPoint location, RateIt.Common.Core.Constants.StoreQueryAreaLevel areaLevel) {
            return base.Channel.GetStoresAtLocation(sessionInfo, location, areaLevel);
        }
    }
}
