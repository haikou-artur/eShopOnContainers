namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.Events;

/// <summary>
/// Event used when the order stock items are confirmed
/// </summary>
public class OrderStatusChangedToCouponConfirmedDomainEvent
    : INotification
{
    public int OrderId { get; }

    public OrderStatusChangedToCouponConfirmedDomainEvent(int orderId)
        => OrderId = orderId;
}
