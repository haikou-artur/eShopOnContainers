using Ordering.API.Application.Commands;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderPointsConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderPointsConfirmedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public OrderPointsConfirmedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderPointsConfirmedIntegrationEvent @event)
        {
            var command = new SetPointsConfirmedStatusCommand(@event.OrderId);

            await _mediator.Send(command);
        }
    }
}
