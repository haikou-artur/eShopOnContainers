using Coupon.Domain.Events;
using Coupon.Domain.Interfaces;
using Coupon.Domain.Interfaces.Services;
using Coupon.Domain.Models;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;

namespace Coupon.Domain.EventHandlers
{
    public class OrderStatusChangedToAwaitingPointsValidationIntegrationEventHandler
        : IIntegrationEventHandler<OrderStatusChangedToAwaitingPointsValidationIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILoyaltyProgramService _service;

        public OrderStatusChangedToAwaitingPointsValidationIntegrationEventHandler(IEventBus eventBus, ILoyaltyProgramService service)
        {
            _eventBus= eventBus;
            _service= service;
        }

        public async Task Handle(OrderStatusChangedToAwaitingPointsValidationIntegrationEvent @event)
        {
            var loyalty = await _service.GetLoyaltyPoint(@event.BuyerId);

            _ = loyalty switch
            {
                null => SendSuccessEvent(@event.OrderId),
                { Balance: var x } when x - @event.Points >= 0 => await UpdatePoints(loyalty, @event),
                _ => SendUnsuccessEvent(@event.OrderId)
            };
        }

        private bool SendSuccessEvent(long orderId)
        {
            var successEvent = new OrderPointsConfirmedIntegrationEvent
            {
                OrderId = orderId
            };

            _eventBus.Publish(successEvent);
            return true;
        }

        private async Task<bool> UpdatePoints(LoyaltyPoint loyalty, OrderStatusChangedToAwaitingPointsValidationIntegrationEvent @event)
        {
            loyalty.Balance -= @event.Points;
            await _service.UpdateLoyaltyPoint(loyalty);

            return SendSuccessEvent(@event.OrderId);
        }

        private bool SendUnsuccessEvent(long orderId)
        {
            var successEvent = new OrderPointsRejectedIntegrationEvent
            {
                OrderId = orderId
            };

            _eventBus.Publish(successEvent);
            return true;
        }
    }
}
