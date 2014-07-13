namespace RateIt.Common.Core.ErrorCodes
{
    public static class ECStoreRegistration
    {
        public const int StoreNameIsBlank = 1;
        public const int MinStoreNameLengthRequired = 2;
        public const int MaxStoreNameLengthExceeded = 3;
        public const int MaxAddressLengthExceeded = 4;
        public const int MaxDescriptionLengthExceeded = 5;
        public const int InvalidGeoCoordinates = 6;
    }
}
