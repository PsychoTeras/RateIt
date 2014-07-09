using RateIt.Common.Classes;
using RateIt.Common.Core.QueryParams;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Common.Core.Controller
{
    public interface IRateItControllerSys
    {

#region User actions

        BaseQueryResult     UserLogoutSys(QuerySysRequestID sysId, string userName);
        UserListQueryResult GetUserListSys(QuerySysRequestID sysId, string userNamePart, uint maxCount);

#endregion

#region Store action

        StoreListQueryResult GetStoresAtLocationSys(QuerySysRequestID sysId, GeoRectangle rectangle);

#endregion

    }
}
