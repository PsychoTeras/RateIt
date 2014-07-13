using System;
using MongoDB.Bson;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    internal class UserLogged : BaseDocument
    {

#region Public fields

        public ObjectId UserId;
        public DateTime LastAccessTime;

#endregion

#region Class methods

        public UserLogged() {}

        public UserLogged(ObjectId userId)
        {
            ResetLastAccessTime();
            UserId = userId;
        }

        public UserLogged(User user) 
        {
            ResetLastAccessTime();
            UserId = user.Id;
        }

        public DateTime ResetLastAccessTime()
        {
            double min = GenericConstants.USER_SESSION_TTL_MIN;
            LastAccessTime = DateTime.UtcNow.AddMinutes(min);
            return LastAccessTime;
        }

#endregion

    }
}
