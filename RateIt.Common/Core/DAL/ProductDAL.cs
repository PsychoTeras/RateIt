using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RateIt.Common.Core.Entities.Products;

namespace RateIt.Common.Core.DAL
{
    internal sealed class ProductDAL : BaseDAL<Product>
    {

#region Constants

        internal const string IDX_T_PRODUCTS_PRODUCT_CODE = "IDX_T_PRODUCTS_PRODUCT_CODE";
        internal const string IDX_T_PRODUCTS_STORE_ID_PRODUCT_NAME = "IDX_T_PRODUCTS_STORE_ID_PRODUCT_NAME";

#endregion

#region Properties

        internal override string CollectionName
        {
            get { return "T_PRODUCTS"; }
        }

#endregion

#region Class methods

        protected override void CreateCollectionStructure()
        {
            //Create T_PRODUCTS indexes, IDX_T_PRODUCTS_PRODUCT_CODE
            IndexKeysBuilder indexKeys = IndexKeys.
                Ascending("ProductCode");
            IndexOptionsBuilder indexOptions = IndexOptions.
                SetName(IDX_T_PRODUCTS_PRODUCT_CODE).
                SetUnique(true);
            DataCollection.CreateIndex(indexKeys, indexOptions);

            //IDX_T_PRODUCTS_STORE_ID_PRODUCT_NAME
            indexKeys = IndexKeys.
                Ascending("StoreId").
                Ascending("ProductName");
            indexOptions = IndexOptions.
                SetName(IDX_T_PRODUCTS_STORE_ID_PRODUCT_NAME);
            DataCollection.CreateIndex(indexKeys, indexOptions);
        }

        public void ProductRegister(Product product)
        {
            //Register new user
            WriteConcernResult concernResult = DataCollection.Insert(product);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);
        }

        public Product[] GetProducts(ObjectId storeId, string productKeyWord, uint maxCount)
        {
            //Search by part of product name, storeId and keywords
            string queryString = string.Format("/({0})/(si)", productKeyWord);
            IMongoQuery qProductForStoreByName = new QueryBuilder<Product>().And(new[]
                {
                    Query.EQ("StoreId", storeId),
                    Query.Matches("ProductName", queryString)
                });

            //Do search
            MongoCursor<Product> cursor = DataCollection.
                Find(qProductForStoreByName).
                SetHint(IDX_T_PRODUCTS_STORE_ID_PRODUCT_NAME);

            //Set TOP (N) limit
            if (maxCount > 0)
            {
                cursor = cursor.SetLimit((int)maxCount);
            }

            //Get result list
            Product[] result = cursor.ToArray();

            //Return list of users
            return result;            
        }
        
#endregion

    }
}
