using System;

namespace RateIt.Common.Classes
{
    [Serializable]
    public sealed class GeoPoint
    {

#region Public fields

        public double Latitude;
        public double Longitude;

#endregion

#region Ctor

        public GeoPoint() {}

        public GeoPoint(GeoPoint geoPoint)
        {
            if (geoPoint != null)
            {
                Latitude = geoPoint.Latitude;
                Longitude = geoPoint.Longitude;
            }
        }

        public GeoPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

#endregion

#region Class methods

        public override int GetHashCode()
        {
            return (int)(Latitude * Longitude);
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", Latitude, Longitude);
        }

#endregion

    }
}
