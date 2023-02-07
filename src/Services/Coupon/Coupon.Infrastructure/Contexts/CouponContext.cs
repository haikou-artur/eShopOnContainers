using Coupon.Domain.Models;
using Coupon.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Coupon.Infrastructure.Contexts
{
    public class CouponContext
    {
        public IMongoCollection<Domain.Models.Coupon> Coupons;
        public IMongoCollection<LoyaltyPoint> Points;

        public CouponContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            {
                MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
                IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
                Coupons = database.GetCollection<Domain.Models.Coupon>(mongoDBSettings.Value.CouponCollectionName);
                Points = database.GetCollection<LoyaltyPoint>(mongoDBSettings.Value.LoyaltyPointCollectionName);
            }
        }
    }
}
