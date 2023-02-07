using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.Domain.Events
{
    public record OrderStatusChangedToAwaitingCouponValidationIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public string CouponCode { get; set; }
    }
}
