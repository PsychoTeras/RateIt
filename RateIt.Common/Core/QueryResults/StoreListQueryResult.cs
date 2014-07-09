using System;
using RateIt.Common.Core.Entities.Stores;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public sealed class StoreListQueryResult : BaseQueryResult
    {

#region Public fields

        public Store[] Stores;

#endregion

#region Class methods

        public StoreListQueryResult() { }

        public StoreListQueryResult(Store[] stores)
        {
            Stores = stores;
        }

#endregion

    }
 }
