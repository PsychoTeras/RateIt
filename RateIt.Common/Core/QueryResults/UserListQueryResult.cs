using System;
using RateIt.Common.Core.Entities.Users;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public sealed class UserListQueryResult : BaseQueryResult
    {

#region Public fields

        public User[] UserList;

#endregion

#region Class methods

    public UserListQueryResult() { }

    public UserListQueryResult(User[] userList)
    {
        UserList = userList;
    }

#endregion

    }
}
