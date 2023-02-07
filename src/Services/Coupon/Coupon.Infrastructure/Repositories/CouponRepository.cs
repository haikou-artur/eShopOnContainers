using Coupon.Domain.Interfaces;
using Coupon.Infrastructure.Configuration;
using Coupon.Infrastructure.Contexts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Coupon.Infrastructure.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly CouponContext _context;

        public CouponRepository(CouponContext context)
        {
            _context = context;
        }
        public async Task<Domain.Models.Coupon> FindCouponByCodeAsync(string code)
        {
            return await _context.Coupons.Find(x => x.Code == code).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Domain.Models.Coupon coupon)
        {
            await _context.Coupons.ReplaceOneAsync(x => x.Id == coupon.Id, coupon);
        }

        public async Task CreateAsync(Domain.Models.Coupon coupon)
        {
           await _context.Coupons.InsertOneAsync(coupon);
        }
    }
}
