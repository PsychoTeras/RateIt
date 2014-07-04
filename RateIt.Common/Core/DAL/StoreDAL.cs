using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RateIt.Common.Core.Entities.Stores;

namespace RateIt.Common.Core.DAL
{
    internal sealed class StoreDAL : BaseDAL<Store>
    {

#region Constants

        internal const string IDX_T_STORES_STORENAME = "IDX_T_STORES_STORENAME";
        internal const string IDX_T_STORES_LOCATION = "IDX_T_STORES_LOCATION";

#endregion

#region Properties

        internal override string CollectionName
        {
            get { return "T_STORES"; }
        }

#endregion

#region Class methods

        protected override void CreateCollectionStructure()
        {
            //Create T_STORES indexes, IDX_T_STORES_STORENAME
            IndexKeysBuilder indexKeys = IndexKeys.
                Ascending("StoreName");
            IndexOptionsBuilder indexOptions = IndexOptions.
                SetName(IDX_T_STORES_STORENAME).
                SetUnique(false);
            DataCollection.EnsureIndex(indexKeys, indexOptions);

            //IDX_T_STORES_STORENAME
            indexKeys = IndexKeys.
                Ascending("Location");
            indexOptions = IndexOptions.
                SetName(IDX_T_STORES_LOCATION).
                SetUnique(false);
            DataCollection.EnsureIndex(indexKeys, indexOptions);

            //!!! What is it?
            //Server.IndexCache.Add()
        }

        public void StoreRegister(Store registrationInfo)
        {
            //Register new store
            WriteConcernResult concernResult = DataCollection.Insert(registrationInfo);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);
        }

#endregion

     }
}
