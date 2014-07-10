using System;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    public sealed class UserSessionInfo
    {

#region Public fields

        public string UserId;
        public string SessionId;

#endregion

#region Class methods

        public UserSessionInfo() { }

        public UserSessionInfo(string userId, string sessionId)
        {
            if (string.IsNullOrEmpty(UserId = userId))
            {
                throw new ArgumentException("User ID is empty");
            }
            if (string.IsNullOrEmpty(SessionId = sessionId))
            {
                throw new ArgumentException("Session ID is empty");
            }
        }

#endregion

    }
}
