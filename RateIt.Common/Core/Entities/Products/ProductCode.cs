using System;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Entities.Products
{
    [Serializable]
    public struct ProductCode
    {

#region Public fields

        public ProductCodeType Type;
        public byte[] Code;

#endregion

#region Ctor

        public ProductCode(ProductCodeType type, byte[] code)
        {
            Type = type;
            Code = code;
        }

#endregion

    }
}
