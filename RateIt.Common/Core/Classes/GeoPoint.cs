using System;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Classes
{
    [Serializable]
    public sealed class GeoPoint
    {

#region Public fields

        public double Latitude;
        public double Longitude;

#endregion

#region Properties

        public bool IsValid
        {
            get
            {
                return Latitude >= GenericConstants.LATITUDE_MIN &&
                       Latitude <= GenericConstants.LATITUDE_MAX &&
                       Longitude >= GenericConstants.LONGITUDE_MIN &&
                       Longitude <= GenericConstants.LONGITUDE_MAX;
            }
        }

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
