using System;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    public sealed class UserLoginInfo
    {

#region Properties

        public string UserName;
        public string PasswordHash;

#endregion

#region Class methods

        public UserLoginInfo() {}

        public UserLoginInfo(string userName, string passwordHash)
        {
            UserName = userName;
            PasswordHash = passwordHash;
        }

#endregion

    }
}
