using System;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    public sealed class UserSessionInfo
    {

#region Public fields

        public byte[] UserId;
        public byte[] SessionId;

#endregion

#region Class methods

        public UserSessionInfo() { }

        public UserSessionInfo(byte[] userId, byte[] sessionId)
        {
            UserId = userId;
            SessionId = sessionId;
        }

#endregion

    }
}
