using Ordering.API.Application.Commands;
using Ordering.API.Application.IntegrationEvents.Events;

namespace Ordering.API.Application.IntegrationEvents.EventHandling
{
    public class OrderPointsRejectedIntegrationEventHandler : IIntegrationEventHandler<OrderPointsRejectedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public OrderPointsRejectedIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderPointsRejectedIntegrationEvent @event)
        {
            var command = new SetPointsRejectedStatusCommand(@event.OrderId);

            await _mediator.Send(command);
        }
    }
}
