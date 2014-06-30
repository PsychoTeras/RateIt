using System;
using System.Xml.Serialization;

namespace RateIt.Common.Classes
{
    [Serializable]
    public sealed class GeoSize
    {

#region Private fields

        private uint _width;
        private uint _height;

        private GeoArea _cachedGeoArea;

#endregion

#region Properties

        [XmlIgnore]
        public uint Width
        {
            get { return _width; }
            set
            {
                _width = value;
                _cachedGeoArea = null;
            }
        }

        [XmlIgnore]
        public uint Height
        {
            get { return _height; }
            set
            {
                _height = value;
                _cachedGeoArea = null;
            }
        }

#endregion

#region Ctor

        public GeoSize() { }

        public GeoSize(uint width, uint height)
        {
            _width = width;
            _height = height;
        }

#endregion

#region Class methods

        public GeoArea ToGeoArea(GeoPoint centerPoint)
        {
            return _cachedGeoArea ?? (_cachedGeoArea = GeoArea.Rectangle(centerPoint, _width, _height));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GeoSize))
            {
                return false;
            }
            GeoSize comp = (GeoSize)obj;
            return (comp._width == _width) &&
                   (comp._height == _height);
        }

        public override int GetHashCode()
        {
            return (int)(_width ^ (_height << 13) | (_height >> 19));
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", _width, _height);
        }

#endregion

    }
}
