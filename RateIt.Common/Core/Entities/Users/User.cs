using System;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    public class User : BaseDocument
    {

#region Fields

        public string UserName;
        public string Password;
        public string Email;

#endregion

#region Class methods

        public User() : this(string.Empty, string.Empty, string.Empty) { }

        public User(string userName, string password, string email)
        {
            UserName = userName ?? string.Empty;
            Password = password ?? string.Empty;
            Email = email ?? string.Empty;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", UserName, Email);
        }

#endregion

    }
}
