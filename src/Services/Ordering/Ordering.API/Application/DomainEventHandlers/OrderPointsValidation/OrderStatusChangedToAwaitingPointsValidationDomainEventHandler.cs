using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.Domain.Events;

namespace Ordering.API.Application.DomainEventHandlers.OrderPointsValidation
{
    public class OrderStatusChangedToAwaitingPointsValidationDomainEventHandler : INotificationHandler<OrderStatusChangedToAwaitingPointsValidationDomainEvent>
    {
        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
        private readonly IBuyerRepository _buyerRepository;

        public OrderStatusChangedToAwaitingPointsValidationDomainEventHandler(IOrderingIntegrationEventService orderingIntegrationEventService, IBuyerRepository buyerRepository)
        {
            _orderingIntegrationEventService= orderingIntegrationEventService;
            _buyerRepository= buyerRepository;
        }
        public async Task Handle(OrderStatusChangedToAwaitingPointsValidationDomainEvent notification, CancellationToken cancellationToken)
        {
            var buyer = await _buyerRepository.FindByIdAsync(notification.BuyerId.ToString());
            var pointsEvent = new OrderStatusChangedToAwaitingPointsValidationIntegrationEvent
            {
                BuyerId = buyer.IdentityGuid,
                Points = notification.Points,
                OrderId = notification.OrderId
            };

            await _orderingIntegrationEventService.AddAndSaveEventAsync(pointsEvent);
        }
    }
}
