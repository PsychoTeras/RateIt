using System;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public class UserLoginQueryResult : BaseQueryResult
    {

#region Properties

        public byte[] UserId;
        public byte[] SessionId;

#endregion

#region Class methods

    public UserLoginQueryResult() { }

    public UserLoginQueryResult(byte[] userId, byte[] sessionId)
    {
        UserId = userId;
        SessionId = sessionId;
    }

#endregion

    }
}
