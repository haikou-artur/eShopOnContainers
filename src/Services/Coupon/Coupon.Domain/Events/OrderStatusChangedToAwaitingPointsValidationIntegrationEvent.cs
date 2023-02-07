using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.Domain.Events
{
    public record OrderStatusChangedToAwaitingPointsValidationIntegrationEvent : IntegrationEvent
    {
        public long OrderId { get; set; }

        public string BuyerId { get; set; }

        public decimal Points { get; set; }
    }
}
