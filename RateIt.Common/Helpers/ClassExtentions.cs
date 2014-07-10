using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using RateIt.Common.Core.Entities.Stores;

namespace RateIt.Common.Helpers
{
    public static class ClassExtentions
    {
        public static bool IsEmpty(this ObjectId objectId)
        {
            return objectId == ObjectId.Empty;
        }

        public static Store[] ByStoreNameFuzzy(this MongoCursor<Store> cursor, string storeName)
        {
            int fuzzyMatchingTreshold = 2;

            List<Store> stores = new List<Store>();
            if (cursor.Any() && !string.IsNullOrEmpty(storeName))
            {
                foreach (Store store in cursor)
                {
                    if (InternalHelper.FuzzyMatchingForTwoString(store.StoreName, storeName, fuzzyMatchingTreshold))
                    {
                        stores.Add(store);
                    }
                }
            }

            return stores.ToArray();
        }

        public static ObjectId ToObjectId(this string str)
        {
            return new ObjectId(str);
        }
    }
}
