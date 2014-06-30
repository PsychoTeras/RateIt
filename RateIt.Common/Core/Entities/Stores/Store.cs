using System;
using RateIt.Common.Classes;

namespace RateIt.Common.Core.Entities.Stores
{
    [Serializable]
    public sealed class Store : BaseDocument
    {

#region Properties

        public string StoreName;
        public string Address;
        public string Description;
        public GeoPoint Location;
        public GeoSize Size;

#endregion

#region Class methods

        public Store() : this(string.Empty, string.Empty, string.Empty, null, null) { }

        public Store(string storeName, string address, string description,
                     GeoPoint location, GeoSize size)
        {
            StoreName = storeName ?? string.Empty;
            Address = address ?? string.Empty;
            Description = description ?? string.Empty;
            Location = location ?? new GeoPoint();
            Size = size ?? new GeoSize();
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", StoreName, Address);
        }

#endregion

    }
}
