using Coupon.Domain.Interfaces;
using Coupon.Domain.Models;
using Coupon.Infrastructure.Contexts;
using MongoDB.Driver;

namespace Coupon.Infrastructure.Repositories
{
    public class LoyaltyProgramRepository : ILoyaltyProgramRepository
    {
        private readonly CouponContext _context;

        public LoyaltyProgramRepository(CouponContext context)
        {
            _context = context;
        }

        public async Task CreateLoyaltyPoint(LoyaltyPoint loyaltyPoint)
        {
            await _context.Points.InsertOneAsync(loyaltyPoint);
        }

        public async Task<LoyaltyPoint> GetLoyaltyPoint(string userId)
        {
            return await _context.Points.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task UpdateLoyaltyPoint(LoyaltyPoint loyaltyPoint)
        {
            await _context.Points.ReplaceOneAsync(x => x.Id == loyaltyPoint.Id, loyaltyPoint);
        }
    }
}
