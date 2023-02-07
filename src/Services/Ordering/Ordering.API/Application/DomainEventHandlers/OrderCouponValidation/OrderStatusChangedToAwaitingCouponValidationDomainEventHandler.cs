using Ordering.API.Application.IntegrationEvents.Events;
using Ordering.Domain.Events;

namespace Ordering.API.Application.DomainEventHandlers.OrderCouponValidation
{
    public class OrderStatusChangedToAwaitingCouponValidationDomainEventHandler : INotificationHandler<OrderStatusChangedToAwaitingCouponValidationDomainEvent>
    {
        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

        public OrderStatusChangedToAwaitingCouponValidationDomainEventHandler(IOrderingIntegrationEventService orderingIntegrationEventService)
        {
            _orderingIntegrationEventService = orderingIntegrationEventService;
        }

        public async Task Handle(OrderStatusChangedToAwaitingCouponValidationDomainEvent notification, CancellationToken cancellationToken)
        {
            var couponEvent = new OrderStatusChangedToAwaitingCouponValidationIntegrationEvent
            {
                OrderId = notification.OrderId,
                CouponCode = notification.CouponCode
            };

            await _orderingIntegrationEventService.AddAndSaveEventAsync(couponEvent);
        }
    }
}
