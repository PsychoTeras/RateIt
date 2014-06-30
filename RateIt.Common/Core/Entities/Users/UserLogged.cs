using System;
using MongoDB.Bson;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    internal class UserLogged : BaseDocument
    {

#region Fields

        public ObjectId UserId;
        public DateTime LastLoginTime;

#endregion

#region Class methods

        public UserLogged()
        {
            LastLoginTime = DateTime.Now.ToUniversalTime();
        }

        public UserLogged(ObjectId userId) : this()
        {
            UserId = userId;
        }

        public UserLogged(User user) : this()
        {
            if (user == null)
            {
                throw new NullReferenceException("User is null-reference");
            }
            UserId = user.Id;
        }

#endregion

    }
}
