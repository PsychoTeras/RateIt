﻿using System;
using System.Linq;
using RateIt.Common.Core.Classes;
using RateIt.Common.Core.Constants;
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
            int i = 1, y = 2;
            int hc1 = i.GetHashCode(), hc2 = y.GetHashCode();
            var t1 = i.GetTypeCode();

            string passwordHash = CommonHelper.GetHashSum("1");
            UserLoginInfo loginInfo = new UserLoginInfo("1", passwordHash);
            UserLoginQueryResult loginResult = _service.UserLogin(loginInfo);
            if (!loginResult.HasError)
            {
                UserSessionInfo sessionInfo = new UserSessionInfo(Convert.FromBase64String(loginResult.UserId), Convert.FromBase64String(loginResult.SessionId));
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
