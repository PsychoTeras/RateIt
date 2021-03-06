﻿using System;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Classes
{
    [Serializable]
    public sealed class GeoSize
    {

#region Public fields

        public uint Width;
        public uint Height;

#endregion

#region Properties

        public bool IsValid
        {
            get
            {
                return Width >= GenericConstants.STORE_SIZE_MIN_M &&
                       Width <= GenericConstants.STORE_SIZE_MAX_M &&
                       Height >= GenericConstants.STORE_SIZE_MIN_M &&
                       Height <= GenericConstants.STORE_SIZE_MAX_M;
            }
        }

#endregion

#region Ctor

        public GeoSize() { }

        public GeoSize(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

#endregion

#region Class methods

        public GeoArea ToGeoArea(GeoPoint centerPoint)
        {
            return GeoArea.Rectangle(centerPoint, Width, Height);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GeoSize))
            {
                return false;
            }
            GeoSize comp = (GeoSize)obj;
            return comp.Width == Width && comp.Height == Height;
        }

        public override int GetHashCode()
        {
            return (int)(Width ^ (Height << 13) | (Height >> 19));
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", Width, Height);
        }

#endregion

    }
}
