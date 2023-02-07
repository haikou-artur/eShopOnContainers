using Ordering.API.Application.Commands;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderCouponConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderCouponConfirmedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public OrderCouponConfirmedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderCouponConfirmedIntegrationEvent @event)
        {
            var command = new SetCouponConfirmedStatusCommand(@event.OrderId);

            await _mediator.Send(command);
        }
    }
}
