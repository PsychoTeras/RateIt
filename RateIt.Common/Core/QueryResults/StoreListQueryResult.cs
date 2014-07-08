using System;
using RateIt.Common.Core.Entities.Stores;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable]
    public sealed class StoreListQueryResult : BaseQueryResult
    {

#region Properties

        public Store[] Stores { get; internal set; }

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
