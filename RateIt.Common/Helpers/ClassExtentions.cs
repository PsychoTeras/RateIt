using MongoDB.Bson;

namespace RateIt.Common.Helpers
{
    public static class ClassExtentions
    {
        public static bool IsEmpty(this ObjectId objectId)
        {
            return objectId == ObjectId.Empty;
        }
    }
}
