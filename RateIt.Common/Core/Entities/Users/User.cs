using System;
using MongoDB.Bson.Serialization.Attributes;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    public class User : BaseDocument
    {

#region Public fields

        public string UserName;
        public string PasswordHash;
        public string Email;

        public UserState UserState;

        [BsonIgnore]
        public bool IsUserLogged;
        [BsonIgnore]
        public string UserId;

#endregion

#region Class methods

        public User() { }

        protected override void IdHasChanged()
        {
            UserId = Id.ToString();
        }

        public User(string userName, string passwordHash, string email)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", UserName, Email);
        }

#endregion

    }
}
