using System;

namespace RateIt.Common.Core.Entities.Users
{
    [Serializable]
    public sealed class UserListItem : User
    {

#region Public fields

        public bool IsUserLogged;

#endregion

#region Class methods

        public UserListItem() {}

#endregion

    }
}
