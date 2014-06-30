using System;
using RateIt.Common.Classes;

namespace RateIt.Common.Helpers
{
    public static class GeoHelper
    {
        public static int EARTH_RADIUS_KM = 6371;

        public static GeoPoint ShiftCoordinates(GeoPoint location,
            int distanceInMetersX, int distanceInMetersY)
        {
            return ShiftCoordinates(location, distanceInMetersX / 1000d, distanceInMetersY / 1000d);
        }

        public static GeoPoint ShiftCoordinates(GeoPoint location, double distanceInKilometersX, double distanceInKilometersY)
        {
            double latitude = location.Latitude + (distanceInKilometersY / EARTH_RADIUS_KM) * 180d / Math.PI;
            double longitude = location.Longitude + (distanceInKilometersX / (EARTH_RADIUS_KM * Math.Cos(latitude * 180d / Math.PI))) * 180d / Math.PI;
            return new GeoPoint(latitude, longitude);
        }
    }
}