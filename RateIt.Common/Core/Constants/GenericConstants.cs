namespace RateIt.Common.Core.Constants
{
    internal static class GenericConstants
    {
        // --- DB ---
        public const double USER_SESSION_TTL_MIN = 120; //2 hours

        // --- Geo ---
        public const int    EARTH_RADIUS_MID_M = 6371302;
        public const int    EARTH_RADIUS_MAX_M = 6378137;
        public const double LATITUDE_MIN       = -85d;
        public const double LATITUDE_MAX       = 85d;
        public const double LONGITUDE_MIN      = -180d;
        public const double LONGITUDE_MAX      = 180d;

        //User
        public const byte USER_NAME_LENGTH_MIN      = 3;
        public const byte USER_NAME_LENGTH_MAX      = 15;
        public const byte USER_PASSWORD_HASH_LENGTH = 40;
        public const byte USER_EMAIL_LENGTH_MAX     = 254;

        //Store
        public const byte STORE_NAME_LENGTH_MIN        = 3;
        public const byte STORE_NAME_LENGTH_MAX        = 50;
        public const byte STORE_ADDRESS_LENGTH_MAX     = 100;
        public const uint STORE_DESCRIPTION_LENGTH_MAX = 1000;
        public const uint STORE_SIZE_MIN_M             = 5;
        public const uint STORE_SIZE_MAX_M             = 2000;

        //Product
        public const byte PRODUCT_NAME_LENGTH_MIN = 3;
        public const byte PRODUCT_NAME_LENGTH_MAX = 50;

        //ObjectId
        public const byte OBJECT_ID_LENGTH = 12;
    }
}
