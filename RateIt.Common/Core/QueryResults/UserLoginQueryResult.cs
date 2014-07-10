using System;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public class UserLoginQueryResult : BaseQueryResult
    {

#region Properties

        public string UserId;
        public string UserName;
        public string SessionId;

#endregion

#region Class methods

    public UserLoginQueryResult() { }

    public UserLoginQueryResult(string userId, string userName, string sessionId)
    {
        UserId = userId;
        UserName = userName;
        SessionId = sessionId;
    }

#endregion

    }
}
