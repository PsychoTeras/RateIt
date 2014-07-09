using System;
using RateIt.Common.Core.Entities.Users;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public sealed class UserListQueryResult : BaseQueryResult
    {

#region Public fields

        public UserListItem[] UserList;

#endregion

#region Class methods

    public UserListQueryResult() { }

    public UserListQueryResult(UserListItem[] userList)
    {
        UserList = userList;
    }

#endregion

    }
}
