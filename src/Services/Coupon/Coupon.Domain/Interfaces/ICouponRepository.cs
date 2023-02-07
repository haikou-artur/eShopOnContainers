namespace Coupon.Domain.Interfaces
{
    public interface ICouponRepository
    {
        Task<Models.Coupon> FindCouponByCodeAsync(string code);

        Task UpdateAsync(Models.Coupon coupon);

        Task CreateAsync(Models.Coupon coupon);
    }
}
