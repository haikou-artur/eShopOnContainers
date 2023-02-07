namespace Microsoft.eShopOnContainers.Payment.API.IntegrationEvents.Events;
    
public record OrderStatusChangedToCouponConfirmedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }

    public OrderStatusChangedToCouponConfirmedIntegrationEvent(int orderId)
        => OrderId = orderId;
}
