namespace Microsoft.eShopOnContainers.Services.Ordering.SignalrHub.IntegrationEvents.EventHandling;

public class OrderStatusChangedToCouponConfirmedIntegrationEventHandler :
    IIntegrationEventHandler<OrderStatusChangedToCouponConfirmedIntegrationEvent>
{
    private readonly IHubContext<NotificationsHub> _hubContext;
    private readonly ILogger<OrderStatusChangedToCouponConfirmedIntegrationEventHandler> _logger;

    public OrderStatusChangedToCouponConfirmedIntegrationEventHandler(
        IHubContext<NotificationsHub> hubContext,
        ILogger<OrderStatusChangedToCouponConfirmedIntegrationEventHandler> logger)
    {
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    public async Task Handle(OrderStatusChangedToCouponConfirmedIntegrationEvent @event)
    {
        using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            await _hubContext.Clients
                .Group(@event.BuyerName)
                .SendAsync("UpdatedOrderState", new { OrderId = @event.OrderId, Status = @event.OrderStatus });
        }
    }
}
