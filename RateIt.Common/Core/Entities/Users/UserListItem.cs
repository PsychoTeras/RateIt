using System;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    public sealed class UserListItem : User
    {

#region Fields

        public bool IsUserLogged;

#endregion

#region Class methods

        public UserListItem() {}

#endregion

    }
}
