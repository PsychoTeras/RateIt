using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RateIt.Common.Classes;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.QueryResults;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.DAL
{
    internal sealed class StoreDAL : BaseDAL<Store>
    {

#region Constants

        internal const string IDX_T_STORES_STORENAME = "IDX_T_STORES_STORENAME";
        internal const string IDX_T_STORES_LOCATION = "IDX_T_STORES_LOCATION";

        internal const string GEOPOINT_FIELD_NAME = "Location";

#endregion

#region Properties

        internal override string CollectionName
        {
            get { return "T_STORES"; }
        }

        internal MongoCollection<Store> StoreListDataCollection { get; private set; }

#endregion

#region Class methods

        public StoreDAL()
        {
            StoreListDataCollection = Database.GetCollection<Store>(CollectionName);
        }

        protected override void CreateCollectionStructure()
        {
            //Create T_STORES indexes, IDX_T_STORES_STORENAME
            IndexKeysBuilder indexKeys = IndexKeys.
                Ascending("StoreName");
            IndexOptionsBuilder indexOptions = IndexOptions.
                SetName(IDX_T_STORES_STORENAME);
            DataCollection.EnsureIndex(indexKeys, indexOptions);

            //IDX_T_STORES_STORENAME
            indexKeys = IndexKeys.
                GeoSpatial(GEOPOINT_FIELD_NAME);
            indexOptions = IndexOptions.
                SetName(IDX_T_STORES_LOCATION);
            DataCollection.EnsureIndex(indexKeys, indexOptions);
        }

        public void StoreRegister(Store registrationInfo)
        {
            GetStoresByNameTemplateAndLocation(registrationInfo.StoreName, registrationInfo.Location);

            //Register new store
            WriteConcernResult concernResult = DataCollection.Insert(registrationInfo);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);
        }

        public StoreListQueryResult GetStoresByNameTemplateAndLocation(string storeName, GeoPoint location)
        {
            const double searchRadius = 0.2d; //In km, 200m
            double searchRadiusRad = searchRadius / GeoHelper.EARTH_RADIUS_KM;

            //Search by location
            IMongoQuery qStoresAtLocation = Query.WithinCircle(GEOPOINT_FIELD_NAME,
                location.Latitude, location.Longitude, searchRadiusRad, true);

            //Do search
            MongoCursor<Store> cursor = StoreListDataCollection.
                Find(qStoresAtLocation).
                SetHint(IDX_T_STORES_LOCATION);
            
            //
            Store[] stores = cursor.ByStoreNameFuzzy(storeName);

            //Return result
            return new StoreListQueryResult(stores);
        }

#endregion

     }
}
