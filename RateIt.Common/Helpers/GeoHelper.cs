using System;
using RateIt.Common.Classes;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Helpers
{
    public static class GeoHelper
    {
        public static GeoPoint ShiftCoordinates(GeoPoint location, double distanceInMetersX, double distanceInMetersY)
        {
            double latitude = distanceInMetersX / GeoConstants.EARTH_RADIUS_M;
            double longitude = distanceInMetersY / (GeoConstants.EARTH_RADIUS_M * Math.Cos(location.Latitude * Math.PI / 180d));
            return new GeoPoint
                (
                    location.Latitude + latitude * 180d / Math.PI,
                    location.Longitude - longitude * 180d / Math.PI
                );
        }
    }
}