using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RateIt.Common.Core.Entities
{
    [Serializable]
    public class BaseDocument
    {

#region Internal fields

        [BsonId]
        internal ObjectId Id;

#endregion

#region Class members

        public BaseDocument() { }

#endregion

    }
}
