using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RateIt.Common.Core.Entities
{
    [Serializable]
    public class BaseDocument
    {

#region Properties

        [BsonId]
        public ObjectId Id { get; set; }

#endregion

#region Class members

        public BaseDocument() { }

#endregion

    }
}
