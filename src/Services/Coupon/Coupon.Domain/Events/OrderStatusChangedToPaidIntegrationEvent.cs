using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.Domain.Events
{
    public record OrderStatusChangedToPaidIntegrationEvent : IntegrationEvent
    {
        public string BuyerId { get; set; }
        public decimal Total { get; set; }
    }
}
