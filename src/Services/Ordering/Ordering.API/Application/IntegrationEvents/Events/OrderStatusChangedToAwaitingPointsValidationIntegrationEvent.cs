namespace Ordering.API.Application.IntegrationEvents.Events
{
    public record OrderStatusChangedToAwaitingPointsValidationIntegrationEvent : IntegrationEvent
    {
        public long OrderId { get; set; }

        public string BuyerId { get; set; }

        public decimal Points { get; set; }
    }
}
