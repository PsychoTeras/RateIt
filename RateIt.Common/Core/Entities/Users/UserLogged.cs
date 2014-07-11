using System;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    internal class UserLogged : BaseDocument
    {

#region Public fields

        public string UserId;
        public DateTime LastAccessTime;

#endregion

#region Class methods

        public UserLogged()
        {
            ResetLastAccessTime();
        }

        public UserLogged(string userId)
            : this()
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID is empty");
            }
            UserId = userId;
        }

        public UserLogged(User user) 
            : this()
        {
            if (user == null)
            {
                throw new ArgumentException("User is null-reference");
            }
            if (string.IsNullOrEmpty(user.UserId))
            {
                throw new ArgumentException("User ID is empty");
            }
            UserId = user.UserId;
        }

        public DateTime ResetLastAccessTime()
        {
            double min = GenericConstants.USER_SESSION_TTL_MIN;
            LastAccessTime = DateTime.Now.AddMinutes(min).ToUniversalTime();
            return LastAccessTime;
        }

#endregion

    }
}
