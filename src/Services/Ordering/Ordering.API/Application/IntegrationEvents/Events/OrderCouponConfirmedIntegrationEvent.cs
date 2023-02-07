namespace Ordering.API.Application.IntegrationEvents.Events
{
    public record OrderCouponConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public string CouponCode { get; set; }
    }
}
