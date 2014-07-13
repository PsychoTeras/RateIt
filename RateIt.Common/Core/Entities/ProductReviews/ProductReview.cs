using System;
using System.Security.Cryptography;
using MongoDB.Bson;

namespace RateIt.Common.Core.Entities.ProductReviews
{
    [Serializable]
    public sealed class ProductReview : BaseDocument
    {

#region Public fields

        public ObjectId UserId;
        public ObjectId ProductId;

        public DateTime Date;
        public string Description;
        public byte RatingQuality;
        public byte RatingPriceQaulity;
        public bool IsPublic;

        public short ReviewRating;
        public ushort ReviewVotesCount;

#endregion

#region Ctor

        public ProductReview() { }

        public ProductReview(ObjectId userId, ObjectId productId,
            string description, byte ratingQuality, byte ratingPriceQaulity,
            bool isPublic)
        {
            UserId = userId;
            ProductId = productId;
            Description = description;
            RatingQuality = ratingQuality;
            RatingPriceQaulity = ratingPriceQaulity;
            IsPublic = isPublic;
            Date = DateTime.UtcNow;
        }

#endregion

    }
}
