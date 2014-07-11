using System;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public class UserLoginQueryResult : BaseQueryResult
    {

#region Properties

        public byte[] UserId;
        public string UserName;
        public byte[] SessionId;

#endregion

#region Class methods

    public UserLoginQueryResult() { }

    public UserLoginQueryResult(byte[] userId, string userName, byte[] sessionId)
    {
        UserId = userId;
        UserName = userName;
        SessionId = sessionId;
    }

#endregion

    }
}
