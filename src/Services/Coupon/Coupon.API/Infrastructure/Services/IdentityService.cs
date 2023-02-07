namespace Coupon.API.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserIdentity()
        {
            var t = _context.HttpContext.User.Claims.ToList();
            var t1 = _context.HttpContext.User.Identity;
            return _context.HttpContext.User.FindFirst("sub").Value;
        }
    }
}
