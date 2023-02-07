using Ordering.API.Application.Commands;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderCouponRejectedIntegrationEventHandler : IIntegrationEventHandler<OrderCouponRejectedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public OrderCouponRejectedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderCouponRejectedIntegrationEvent @event)
        {
            var command = new SetCouponRejectedStatusCommand(@event.OrderId);

            await _mediator.Send(command);
        }
    }
}
