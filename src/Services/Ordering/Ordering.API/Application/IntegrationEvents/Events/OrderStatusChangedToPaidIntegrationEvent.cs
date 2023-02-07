namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events;

public record OrderStatusChangedToPaidIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public string OrderStatus { get; }
    public string BuyerName { get; }
    public IEnumerable<OrderStockItem> OrderStockItems { get; }
    public string BuyerId { get; set; }
    public decimal Total { get; set; }

    public OrderStatusChangedToPaidIntegrationEvent(int orderId,
        string orderStatus,
        string buyerName,
        IEnumerable<OrderStockItem> orderStockItems,
        decimal total,
        string buyerId)
    {
        OrderId = orderId;
        OrderStockItems = orderStockItems;
        OrderStatus = orderStatus;
        BuyerName = buyerName;
        Total = total;
        BuyerId = buyerId;
    }
}

