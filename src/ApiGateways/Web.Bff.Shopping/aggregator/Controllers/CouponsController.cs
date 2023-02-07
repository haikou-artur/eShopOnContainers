namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Controllers
{
    [Route("cp/api/[controller]")]
    [Authorize]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;
        public CouponsController(ICouponService couponService)
        {
            _couponService= couponService;
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<Coupon>> Get(string code)
        {
            var coupon = await _couponService.GetCouponByCodeAsync(code);
            return coupon;
        }
    }
}
