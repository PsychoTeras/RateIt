using System;
using RateIt.Common.Core.Classes;

namespace RateIt.Common.Core.Entities.Stores
{
    [Serializable]
    public sealed class Store : BaseDocument
    {

#region Public fields

        public string StoreId;

        public string StoreName;
        public string Address;
        public string Description;
        public GeoPoint Location;
        public GeoSize Size;

#endregion

#region Properties

#endregion

#region Ctor

        public Store() {}

        public Store(string storeName, string address, string description,
                     GeoPoint location, GeoSize size)
        {
            StoreName = storeName ?? string.Empty;
            Address = address ?? string.Empty;
            Description = description ?? string.Empty;
            Location = location ?? new GeoPoint();
            Size = size ?? new GeoSize();
        }

#endregion

#region Class methods

        protected override void IdHasChanged()
        {
            StoreId = Id.ToString();
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", StoreName, Address);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

#endregion

    }
}
