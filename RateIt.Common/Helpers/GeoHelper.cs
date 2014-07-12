using System;
using RateIt.Common.Core.Classes;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Helpers
{
    public static class GeoHelper
    {
        public static GeoPoint ShiftCoordinates(GeoPoint location, double distanceInMetersX, double distanceInMetersY)
        {
            double latitude = distanceInMetersX / GenericConstants.EARTH_RADIUS_MAX_M;
            double longitude = distanceInMetersY / (GenericConstants.EARTH_RADIUS_MAX_M * Math.Cos(location.Latitude * Math.PI / 180d));
            return new GeoPoint
                (
                    location.Latitude + latitude * 180d / Math.PI,
                    location.Longitude - longitude * 180d / Math.PI
                );
        }
    }
}