using System;

namespace RateIt.Common.Core.Constants
{
    [Flags]
    public enum UserState
    {
        Active = 1,
        Blocked = 2,
        Deleted = 4,
        ReadOnly = 8
    }
}
