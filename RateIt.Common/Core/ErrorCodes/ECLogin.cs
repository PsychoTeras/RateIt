namespace RateIt.Common.Core.ErrorCodes
{
    public static class ECLogin
    {
        public const int UserNameIsBlank = 1;
        public const int MinUserNameLengthRequired = 2;
        public const int MaxUserNameLengthExceeded = 3;
        public const int InvalidUserName = 4;
        public const int PasswordIsBlank = 5;
        public const int InvalidPasswordHash = 6;
        public const int InvalidCrenedtials = 7;
        public const int UserIsLogged = 8;
    }
}
