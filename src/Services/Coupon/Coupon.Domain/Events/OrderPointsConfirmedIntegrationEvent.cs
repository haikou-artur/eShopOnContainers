using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.Domain.Events
{
    public record OrderPointsConfirmedIntegrationEvent : IntegrationEvent
    {
        public long OrderId { get; set; }
    }
}
