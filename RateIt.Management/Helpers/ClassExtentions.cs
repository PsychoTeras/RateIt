using System.Drawing;
using GMap.NET;
using RateIt.Common.Core.Classes;

namespace RateIt.Management.Helpers
{
    public static class ClassExtentions
    {
        public static string ToText(this bool val)
        {
            return val ? "Yes" : "No";
        }

        public static GeoPoint ToGeoPoint(this PointLatLng val)
        {
            return new GeoPoint(val.Lat, val.Lng);
        }

        public static PointLatLng ToPointLatLng(this GeoPoint geoPoint)
        {
            return new PointLatLng(geoPoint.Latitude, geoPoint.Longitude);
        }

        public static PointF ToPointF(this GPoint gPoint)
        {
            return new PointF(gPoint.X, gPoint.Y);
        }
    }
}
