using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.Domain.Events
{
    public record OrderCouponRejectedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public string CouponCode { get; set; }
    }
}
