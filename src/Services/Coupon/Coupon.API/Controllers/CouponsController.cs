using Coupon.API.Infrastructure.Models;
using Coupon.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coupon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponsController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CouponDto>> GetCouponByCodeAsync(string code)
        {
            var coupon = await _couponRepository.FindCouponByCodeAsync(code);
            Console.WriteLine($"{code}:{coupon?.Id}");

            if (coupon is null || coupon.Consumed)
            {
                return NotFound();
            }

            return new CouponDto
            {
                Code = coupon.Code,
                Discount = coupon.Discount
            };
        }

        [HttpPost]
        public async Task<ActionResult> Create(Domain.Models.Coupon coupon)
        {
            await _couponRepository.CreateAsync(coupon);

            return Ok(coupon.Id);
        }
    }
}
