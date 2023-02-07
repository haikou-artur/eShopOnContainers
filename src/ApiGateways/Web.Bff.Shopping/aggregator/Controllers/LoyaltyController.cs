namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Controllers
{
    [Route("l/api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyService _loyaltyService;

        public LoyaltyController(ILoyaltyService loyaltyService)
        {
            _loyaltyService= loyaltyService;
        }

        [HttpGet]
        public async Task<ActionResult<decimal>> Get()
        {
            return await _loyaltyService.GetLoyaltyAsync();
        }
    }
}
