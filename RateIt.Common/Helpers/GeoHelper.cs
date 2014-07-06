using System;
using RateIt.Common.Classes;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Helpers
{
    public static class GeoHelper
    {
        public static GeoPoint ShiftCoordinates(GeoPoint location, int distanceInMetersX, int distanceInMetersY)
        {
            return ShiftCoordinates(location, distanceInMetersX/1000d, distanceInMetersY/1000d);
        }

        public static GeoPoint ShiftCoordinates(GeoPoint location, double distanceInKilometersX, double distanceInKilometersY)
        {
            double latitude = distanceInKilometersY / GeoConstants.EARTH_RADIUS_KM;
            double longitude = distanceInKilometersX / (GeoConstants.EARTH_RADIUS_KM * Math.Cos(location.Latitude * Math.PI / 180d));
            return new GeoPoint
                (
                    location.Latitude + latitude * 180d / Math.PI,
                    location.Longitude - longitude * 180d / Math.PI
                );
        }
    }
}