namespace Ordering.API.Application.IntegrationEvents.Events
{
    public record OrderPointsConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
    }
}
