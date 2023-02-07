namespace Ordering.Domain.Events
{
    public class OrderStatusChangedToAwaitingPointsValidationDomainEvent : INotification
    {
        public long OrderId { get; set; }

        public int BuyerId { get; init; }

        public decimal Points { get; init; }

        public OrderStatusChangedToAwaitingPointsValidationDomainEvent(int buyerId, decimal points, int orderId)
        {
            BuyerId = buyerId;
            Points = points;
            OrderId = orderId;
        }
    }
}
