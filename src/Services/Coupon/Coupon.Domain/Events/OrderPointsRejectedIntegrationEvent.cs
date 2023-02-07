using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Coupon.Domain.Events
{
    public record OrderPointsRejectedIntegrationEvent : IntegrationEvent
    {
        public long OrderId { get; set; }
    }
}
