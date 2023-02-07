namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.DomainEventHandlers.OrderStockConfirmed;

public class OrderStatusChangedToCouponConfirmedDomainEvent
                : INotificationHandler<Domain.Events.OrderStatusChangedToCouponConfirmedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBuyerRepository _buyerRepository;
    private readonly ILoggerFactory _logger;
    private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

    public OrderStatusChangedToCouponConfirmedDomainEvent(
        IOrderRepository orderRepository,
        IBuyerRepository buyerRepository,
        ILoggerFactory logger,
        IOrderingIntegrationEventService orderingIntegrationEventService)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _buyerRepository = buyerRepository ?? throw new ArgumentNullException(nameof(buyerRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderingIntegrationEventService = orderingIntegrationEventService;
    }

    public async Task Handle(Domain.Events.OrderStatusChangedToCouponConfirmedDomainEvent orderStatusChangedToStockConfirmedDomainEvent, CancellationToken cancellationToken)
    {
        _logger.CreateLogger<OrderStatusChangedToCouponConfirmedDomainEvent>()
            .LogTrace("Order with Id: {OrderId} has been successfully updated to status {Status} ({Id})",
                orderStatusChangedToStockConfirmedDomainEvent.OrderId, nameof(OrderStatus.StockConfirmed), OrderStatus.StockConfirmed.Id);

        var order = await _orderRepository.GetAsync(orderStatusChangedToStockConfirmedDomainEvent.OrderId);
        var buyer = await _buyerRepository.FindByIdAsync(order.GetBuyerId.Value.ToString());

        var orderStatusChangedToCouponConfirmedIntegrationEvent = new OrderStatusChangedToCouponConfirmedIntegrationEvent(order.Id, order.OrderStatus.Name, buyer.Name);
        await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStatusChangedToCouponConfirmedIntegrationEvent);
    }
}
