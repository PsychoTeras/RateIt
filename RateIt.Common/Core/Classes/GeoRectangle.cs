using System;

namespace RateIt.Common.Core.Classes
{
    [Serializable]
    public sealed class GeoRectangle
    {

#region Public fields

        public double Latitude;
        public double Longitude;
        public double LatitudeShift;
        public double LongitudeShift;

#endregion

#region Ctor

        public GeoRectangle() { }

        public GeoRectangle(double latitude, double longitude, double latitudeShift, 
                            double longitudeShift)
        {
            Latitude = latitude;
            Longitude = longitude;
            LatitudeShift = latitudeShift;
            LongitudeShift = longitudeShift;
        }

#endregion

#region Class methods

        private bool Equals(GeoRectangle other)
        {
            return Math.Abs(Latitude - other.Latitude) < double.Epsilon &&
                   Math.Abs(Longitude - other.Longitude) < double.Epsilon &&
                   Math.Abs(LatitudeShift - other.LatitudeShift) < double.Epsilon &&
                   Math.Abs(LongitudeShift - other.LongitudeShift) < double.Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj is GeoRectangle && Equals((GeoRectangle)obj);
        }

        public static bool operator ==(GeoRectangle rect1, GeoRectangle rect2)
        {
            return !ReferenceEquals(rect1, null) && !ReferenceEquals(rect2, null) && rect1.Equals(rect2);
        }

        public static bool operator !=(GeoRectangle left, GeoRectangle right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                hashCode = (hashCode * 397) ^ LatitudeShift.GetHashCode();
                hashCode = (hashCode * 397) ^ LongitudeShift.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return "Latitude =" + Latitude + 
                   ", longtitude = " + Longitude +
                   ", latitude shift = " + LatitudeShift + 
                   ", longitude shift = " + LongitudeShift;
        }

#endregion

    }
}