namespace Ordering.API.Application.IntegrationEvents.Events
{
    public record OrderPointsRejectedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
    }
}
