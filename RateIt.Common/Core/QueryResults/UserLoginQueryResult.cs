using System;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public class UserLoginQueryResult : BaseQueryResult
    {

#region Properties

        public string UserName;
        public string SessionId;

#endregion

#region Class methods

    public UserLoginQueryResult() { }

    public UserLoginQueryResult(string userName, string sessionId)
    {
        UserName = userName;
        SessionId = sessionId;
    }

#endregion

    }
}
