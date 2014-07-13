using System;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public class ProductRegisterQueryResult : BaseQueryResult
    {

#region Properties

        public byte[] ProductId;

#endregion

#region Class methods

    public ProductRegisterQueryResult() { }

    public ProductRegisterQueryResult(byte[] productId)
    {
        ProductId = productId;
    }

#endregion

    }
}
