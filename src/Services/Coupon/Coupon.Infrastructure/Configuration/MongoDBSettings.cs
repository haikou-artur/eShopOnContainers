namespace Coupon.Infrastructure.Configuration
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string LoyaltyPointCollectionName { get; set; } = null!;
        public string CouponCollectionName { get; set; } = null!;
    }
}
