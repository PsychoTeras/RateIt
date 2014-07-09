using System;
using MongoDB.Bson;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    internal class UserLogged : BaseDocument
    {

#region Public fields

        public string UserName;
        public ObjectId UserId;
        public DateTime LastAccessTime;

#endregion

#region Class methods

        public UserLogged()
        {
            ResetLastAccessTime();
        }

        public UserLogged(string userName, ObjectId userId)
            : this()
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("User name is empty");
            }
            UserName = userName;
            UserId = userId;
        }

        public UserLogged(User user) 
            : this()
        {
            if (user == null)
            {
                throw new ArgumentException("User is null-reference");
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                throw new ArgumentException("User name is empty");
            }
            UserName = user.UserName;
            UserId = user.Id;
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
