using System;

namespace RateIt.Common.Classes
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

        /*public bool Contains(GeoPoint geoPoint)
        {
            return X <= x &&
                   x < X + Width &&
                   Y <= y &&
                   y < Y + Height;
        }

        public bool Contains(GeoRectangle rect)
        {
            return (X <= rect.X) &&
                   ((rect.X + rect.Width) <= (X + Width)) &&
                   (Y <= rect.Y) &&
                   ((rect.Y + rect.Height) <= (Y + Height));
        }

        public override int GetHashCode()
        {
            return (int) ((UInt32) X ^
                          (((UInt32) Y << 13) | ((UInt32) Y >> 19)) ^
                          (((UInt32) Width << 26) | ((UInt32) Width >> 6)) ^
                          (((UInt32) Height << 7) | ((UInt32) Height >> 25)));
        }

        public void Inflate(int width, int height)
        {
            X -= width;
            Y -= height;
            Width += 2*width;
            Height += 2*height;
        }

        public static GeoRectangle Inflate(GeoRectangle rect, int x, int y)
        {
            GeoRectangle r = rect;
            r.Inflate(x, y);
            return r;
        }

        public void Intersect(GeoRectangle rect)
        {
            GeoRectangle result = Intersect(rect, this);
            X = result.X;
            Y = result.Y;
            Width = result.Width;
            Height = result.Height;
        }

        private GeoRectangle Intersect(GeoRectangle a, GeoRectangle b)
        {
            int x1 = Math.Max(a.X, b.X);
            int x2 = Math.Min(a.X + a.Width, b.X + b.Width);
            int y1 = Math.Max(a.Y, b.Y);
            int y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

            if (x2 >= x1
                && y2 >= y1)
            {

                return new GeoRectangle(x1, y1, x2 - x1, y2 - y1);
            }
            return new GeoRectangle();
        }

        public bool IntersectsWith(GeoRectangle rect)
        {
            return (rect.X < X + Width) &&
                   (X < (rect.X + rect.Width)) &&
                   (rect.Y < Y + Height) &&
                   (Y < rect.Y + rect.Height);
        }

        public static GeoRectangle Union(GeoRectangle a, GeoRectangle b)
        {
            int x1 = Math.Min(a.X, b.X);
            int x2 = Math.Max(a.X + a.Width, b.X + b.Width);
            int y1 = Math.Min(a.Y, b.Y);
            int y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);

            return new GeoRectangle(x1, y1, x2 - x1, y2 - y1);
        }

        public void Offset(int x, int y)
        {
            X += x;
            Y += y;
        }*/
    }

}