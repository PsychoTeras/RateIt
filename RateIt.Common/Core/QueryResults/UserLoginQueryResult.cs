using System;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public class UserLoginQueryResult : BaseQueryResult
    {

#region Properties

        public string UserId;
        public string SessionId;

#endregion

#region Class methods

    public UserLoginQueryResult() { }

    public UserLoginQueryResult(byte[] userId, byte[] sessionId)
    {
        UserId = Convert.ToBase64String(userId);
        SessionId = Convert.ToBase64String(sessionId);
    }

#endregion

    }
}
