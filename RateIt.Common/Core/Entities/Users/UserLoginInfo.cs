using System;
using System.Runtime.Serialization;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    [DataContract]
    public sealed class UserLoginInfo
    {

#region Constants

        public const string DEFAULT_TID = "D69601CC-79B1-46F5-9431-0E32A3C74616";

#endregion

#region Properties

        [DataMember(Order = 1)]
        public string UserName { get; set; }
        [DataMember(Order = 2)]
        public string PasswordHash { get; set; }
        [DataMember(Order = 3)]
        public string TId { get; set; }

#endregion

#region Class methods

        public UserLoginInfo() {}

        public UserLoginInfo(string userName, string passwordHash) :
            this(userName, passwordHash, DEFAULT_TID) {}

        public UserLoginInfo(string userName, string passwordHash, string tId)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            TId = tId;
        }

#endregion

    }
}
