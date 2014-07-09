using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.Entities
{
    [Serializable, DataContract]
    public abstract class BaseDocument
    {

#region Private fields

        protected ObjectId _id;

#endregion

#region Internal fields

        [BsonId, IgnoreDataMember]
        internal ObjectId Id
        {
            get { return _id; }
            set
            {
                _id = value;
                if (!_id.IsEmpty())
                {
                    IdHasChanged();
                }
            }
        }

#endregion

#region Class members

        protected BaseDocument() { }

        protected virtual void IdHasChanged() { }

#endregion

    }
}
