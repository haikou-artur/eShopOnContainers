using Coupon.API.Infrastructure.Services;
using Coupon.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoyaltyProgramController : ControllerBase
    {
        private readonly ILoyaltyProgramService _service;
        private readonly IIdentityService _identityService;

        public LoyaltyProgramController(ILoyaltyProgramService service, IIdentityService identityService)
        {
            _service = service;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<ActionResult<decimal>> Get()
        {
            var userId = _identityService.GetUserIdentity();
            var points = await _service.GetLoyaltyPoint(userId);

            if(points == null)
            {
                return NotFound();
            }

            return points.Balance;
        }
    }
}
