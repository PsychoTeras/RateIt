using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RateIt.Common.Core.Classes;
using RateIt.Common.Core.Constants;
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

#endregion

#region Class methods

        protected override void CreateCollectionStructure()
        {
            //Create T_STORES indexes, IDX_T_STORES_STORENAME
            IndexKeysBuilder indexKeys = IndexKeys.
                Ascending("StoreName");
            IndexOptionsBuilder indexOptions = IndexOptions.
                SetName(IDX_T_STORES_STORENAME);
            DataCollection.CreateIndex(indexKeys, indexOptions);

            //IDX_T_STORES_STORENAME
            indexKeys = IndexKeys.
                GeoSpatial(GEOPOINT_FIELD_NAME);
            indexOptions = IndexOptions.
                SetName(IDX_T_STORES_LOCATION);
            DataCollection.CreateIndex(indexKeys, indexOptions);
        }

        public void StoreRegister(Store registrationInfo)
        {
            //Register new store
            WriteConcernResult concernResult = DataCollection.Insert(registrationInfo);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);
        }

        public StoreListQueryResult GetStoresByNameFuzzyAndLocation(string storeName, GeoPoint location)
        {
            //Calculate search criteria
            const double searchRadius = (double)StoreSize.Huge; //In meters
            const double searchRadiusRad = searchRadius / GenericConstants.EARTH_RADIUS_MAX_M;

            //Search by location
            IMongoQuery qStoresAtLocation = Query.Near(GEOPOINT_FIELD_NAME,
                location.Latitude, location.Longitude, searchRadiusRad, true);

            //Do search
            MongoCursor<Store> cursor = DataCollection.
                Find(qStoresAtLocation).
                SetHint(IDX_T_STORES_LOCATION);
            
            //Get stores, which names are very similar to the template store name
            Store[] stores = cursor.ByStoreNameFuzzy(storeName);

            //Return list of stores
            return new StoreListQueryResult(stores);
        }

        public Store[] GetStoresAtLocation(GeoPoint location, StoreQueryAreaLevel areaLevel)
        {
            double searchRadius = (double)areaLevel;
            double searchRadiusRad = searchRadius / GenericConstants.EARTH_RADIUS_MAX_M;

            //Search by location
            IMongoQuery qStoresAtLocation = Query.Near(GEOPOINT_FIELD_NAME,
                location.Latitude, location.Longitude, searchRadiusRad, true);

            //Do search
            MongoCursor<Store> cursor = DataCollection.
                Find(qStoresAtLocation).
                SetHint(IDX_T_STORES_LOCATION);

            //Get stores
            Store[] stores = cursor.ToArray();

            //Return list of stores
            return stores;
        }

        public Store[] GetStoresAtLocation(GeoRectangle rectangle)
        {
            //Search by area
            IMongoQuery qStoresAtLocation = Query.WithinRectangle(GEOPOINT_FIELD_NAME,
                rectangle.Latitude, rectangle.Longitude,
                rectangle.Latitude + rectangle.LatitudeShift,
                rectangle.Longitude + rectangle.LongitudeShift);

            //Do search
            MongoCursor<Store> cursor = DataCollection.
                Find(qStoresAtLocation).
                SetHint(IDX_T_STORES_LOCATION);

            //Get stores
            Store[] stores = cursor.ToArray();

            //Return list of stores
            return stores;
        }

#endregion

     }
}
