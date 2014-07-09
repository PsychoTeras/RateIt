using System;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    public class User : BaseDocument
    {

#region Public fields

        public string UserName;
        public string PasswordHash;
        public string Email;

#endregion

#region Class methods

        public User() { }

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
