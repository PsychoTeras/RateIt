using System;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Entities.Products
{
    [Serializable]
    public sealed class ProductCode
    {

#region Public fields

        public ProductCodeType Type;
        public byte[] Code;

#endregion

#region Ctor

        public ProductCode() { }

        public ProductCode(ProductCodeType type, byte[] code)
        {
            Type = type;
            Code = code;
        }

#endregion

    }
}
