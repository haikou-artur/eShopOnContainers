using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopOnContainers.WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    public class CouponController : Controller
    {
        [HttpPost]
        public Task<IActionResult> Apply(Microsoft.eShopOnContainers.WebMVC.ViewModels.Order order)
        {
            var coupon = 
            order.SubTotal = order.Total;
            order.Total -=
            return View("Order/Detail", );
        }
    }
}
