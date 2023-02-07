namespace Ordering.API.Application.IntegrationEvents.Events
{
    public record OrderCouponRejectedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public string CouponCode { get; set; }
    }
}
