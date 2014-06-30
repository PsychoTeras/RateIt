using System;
using RateIt.Common.Core.Entities.Users;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public class UserQueryResult : BaseQueryResult
    {

#region Properties

        public User User { get; internal set; }

#endregion

#region Static methods

        public new static UserQueryResult Successful
        {
            get { return new UserQueryResult(); }
        }

#endregion

#region Class methods

    public UserQueryResult() { }

    public UserQueryResult(User user)
    {
        User = user;
    }

#endregion

    }
}
