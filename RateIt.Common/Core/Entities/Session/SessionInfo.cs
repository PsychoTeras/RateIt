using System;

namespace RateIt.Common.Core.Entities.Session
{
    public sealed class SessionInfo
    {

#region Public fields

        public string UserName;
        public string SessionId;

#endregion

#region Class methods

        public SessionInfo() : this(string.Empty, string.Empty) { }

        public SessionInfo(string userName, string sessionId)
        {
            if (string.IsNullOrEmpty(UserName = userName))
            {
                throw new ArgumentException("User name is null or empty");
            }

            if (string.IsNullOrEmpty(SessionId = sessionId))
            {
                throw new ArgumentException("Session ID is null or empty");
            }
        }

        public override string ToString()
        {
            return UserName;
        }

#endregion

    }
}
