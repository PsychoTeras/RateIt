using System.Collections.Generic;
using MongoDB.Bson;

namespace RateIt.MongoDB.Ground.Entities
{
    public sealed class ProductInfo
    {

#region Properties

        //MongoDB reflected properties
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        //Product info
        public string Description { get; set; }

        //Product images
        public byte[] Photo { get; set; }
        public byte[] Thumbnail { get; set; }

        //Need to replace by red-black tree data structure
        public List<string> AssignedKeywords { get; set; }

#endregion

#region Class methods

        //Need to replace by search in red-black tree
        //Null-reference unsafe mathod, case sensitive, non-trimmed
        public bool RelevantlyToKeyword(string keyword)
        {
            return AssignedKeywords.Find(s => s.StartsWith(keyword)) != null;
        }

        public ProductInfo()
        {
            AssignedKeywords = new List<string>();
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", Name ?? string.Empty, Id);
        }

#endregion

    }
}
