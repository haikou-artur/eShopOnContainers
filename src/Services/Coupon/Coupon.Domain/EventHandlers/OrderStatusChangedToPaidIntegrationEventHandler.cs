using Coupon.Domain.Events;
using Coupon.Domain.Interfaces.Services;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;

namespace Coupon.Domain.EventHandlers
{
    public class OrderStatusChangedToPaidIntegrationEventHandler
        : IIntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
    {
        private readonly ILoyaltyProgramService _loyaltyProgramService;

        public OrderStatusChangedToPaidIntegrationEventHandler(ILoyaltyProgramService loyaltyProgramService)
        {
            _loyaltyProgramService= loyaltyProgramService;
        }

        public async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
        {
            await _loyaltyProgramService.ApplyLoyaltyProgram(@event.BuyerId, @event.Total);
        }
    }
}
