using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace RateIt.Common.Core.Entities.Products
{
    [Serializable]
    public sealed class Product : BaseDocument
    {

#region Public fields

        public ObjectId StoreId;

        public ProductCode ProductCode;
        public string ProductName;
        public string Description;

        public byte Rating;

        public List<string> Keywords;

#endregion

#region Class methods

        public Product() { }

        public Product(ObjectId storeId, ProductCode productCode,
            string productName, string description, string[] keywords)
        {
            ProductCode = productCode;
            ProductName = productName;
            Description = description;
            Keywords = keywords != null && keywords.Length > 0
                ? new List<string>(keywords)
                : new List<string>();
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", ProductName ?? string.Empty, Id);
        }

#endregion

    }
}
