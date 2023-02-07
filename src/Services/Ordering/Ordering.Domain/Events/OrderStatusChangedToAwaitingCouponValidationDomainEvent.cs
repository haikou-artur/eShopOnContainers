namespace Ordering.Domain.Events
{
    public record OrderStatusChangedToAwaitingCouponValidationDomainEvent : INotification
    {
        public int OrderId { get; init; }

        public string CouponCode { get; init; }

        public OrderStatusChangedToAwaitingCouponValidationDomainEvent(int orderId, string couponCode)
        {
            OrderId = orderId;
            CouponCode = couponCode;
        }
    }
}
