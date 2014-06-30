using System;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    public sealed class UserLoginInfo
    {

#region Properties

        public string UserName;
        public string Password;

#endregion

#region Class methods

        public UserLoginInfo() : this(string.Empty, string.Empty) {}

        public UserLoginInfo(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

#endregion

    }
}
