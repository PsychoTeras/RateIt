using System;
using RateIt.Common.Core.Classes;

namespace RateIt.Common.Core.Entities.Stores
{
    [Serializable]
    public sealed class Store : BaseDocument
    {

#region Public fields

        public string StoreName;
        public string Address;
        public string Address2;
        public string Description;
        public GeoPoint Location;
        public GeoSize Size;

#endregion

#region Properties

        public byte[] StoreId { get; private set; }

#endregion

#region Ctor

        public Store() {}

        public Store(string storeName, string address, string description,
                     GeoPoint location, GeoSize size)
        {
            StoreName = storeName;
            Address = address;
            Description = description;
            Location = location;
            Size = size;
        }

#endregion

#region Class methods

        protected override void IdHasChanged()
        {
            StoreId = Id.ToByteArray();
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", StoreName, Address);
        }

#endregion

    }
}
