namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services
{
    public interface ICouponService
    {
        Task<Coupon> GetCouponByCodeAsync(string code);
    }
}
