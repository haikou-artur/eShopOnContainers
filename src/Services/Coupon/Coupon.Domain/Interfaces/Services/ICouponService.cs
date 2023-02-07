namespace Coupon.Domain.Interfaces.Services
{
    public interface ICouponService
    {
        Task<Models.Coupon> FindCouponByCodeAsync(string code);

        Task UpdateAsync(Models.Coupon coupon);

        Task CreateAsync(Models.Coupon coupon);
    }
}
