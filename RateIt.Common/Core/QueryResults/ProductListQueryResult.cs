using System;
using RateIt.Common.Core.Entities.Products;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public sealed class ProductListQueryResult : BaseQueryResult
    {

#region Public fields

        public Product[] Products;

#endregion

#region Class methods

        public ProductListQueryResult() { }

        public ProductListQueryResult(Product[] products)
        {
            Products = products;
        }

#endregion

    }
}
