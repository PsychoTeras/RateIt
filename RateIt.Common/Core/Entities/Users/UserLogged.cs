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

        public UserLogged()
        {
            ResetLastAccessTime();
        }

        public UserLogged(ObjectId userId)
            : this()
        {
            UserId = userId;
        }

        public UserLogged(User user) 
            : this()
        {
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
