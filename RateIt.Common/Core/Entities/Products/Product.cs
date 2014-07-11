using System;
using System.Collections.Generic;

namespace RateIt.Common.Core.Entities.Products
{
    [Serializable]
    public sealed class Product : BaseDocument
    {

#region Public fields

        public string Name;
        public string Description;

        public ProductCode ProductCode;

        //Needs to be replaced by red-black tree data structure
        public List<string> AssignedKeywords = new List<string>();

#endregion

#region Class methods

        //Need to replace by search in red-black tree
        //Null-reference unsafe mathod, case sensitive, non-trimmed
        public bool RelevantlyToKeyword(string keyword)
        {
            return AssignedKeywords.Find(s => s.StartsWith(keyword)) != null;
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}]", Name ?? string.Empty, Id);
        }

#endregion

    }
}
