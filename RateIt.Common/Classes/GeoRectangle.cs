using System;

namespace RateIt.Common.Classes
{
    [Serializable]
    public sealed class GeoRectangle
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public GeoRectangle() { }

        public GeoRectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static GeoRectangle FromLTRB(int left, int top, int right, int bottom)
        {
            return new GeoRectangle(left,
                top,
                right - left,
                bottom - top);
        }
        
        public override bool Equals(object obj)
        {
            if (!(obj is GeoRectangle))
                return false;

            GeoRectangle comp = (GeoRectangle) obj;

            return (comp.X == X) &&
                   (comp.Y == Y) &&
                   (comp.Width == Width) &&
                   (comp.Height == Height);
        }

        public static bool operator ==(GeoRectangle left, GeoRectangle right)
        {
            if (left == null && right == null)
            {
                return true;
            }
            if ((left != null && right == null) || left == null)
            {
                return false;
            }
            return (left.X == right.X
                    && left.Y == right.Y
                    && left.Width == right.Width
                    && left.Height == right.Height);
        }

        public static bool operator !=(GeoRectangle left, GeoRectangle right)
        {
            return !(left == right);
        }

        public bool Contains(int x, int y)
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
        }

        public override string ToString()
        {
            return "{X=" + X + ",Y=" + Y + ",Width=" + Width + ",Height=" + Height + "}";
        }
    }

}