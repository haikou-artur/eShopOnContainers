using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Coupon.Domain.Models
{
    public class LoyaltyPoint
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public decimal Balance { get; set; }
    }
}
