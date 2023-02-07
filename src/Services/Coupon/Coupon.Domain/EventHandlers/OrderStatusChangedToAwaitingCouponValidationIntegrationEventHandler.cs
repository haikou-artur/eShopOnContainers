using Coupon.Domain.Events;
using Coupon.Domain.Interfaces;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;

namespace Coupon.Domain.EventHandlers
{
    public class OrderStatusChangedToAwaitingCouponValidationIntegrationEventHandler
        : IIntegrationEventHandler<OrderStatusChangedToAwaitingCouponValidationIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ICouponRepository _couponRepository;

        public OrderStatusChangedToAwaitingCouponValidationIntegrationEventHandler(IEventBus eventBus, ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(OrderStatusChangedToAwaitingCouponValidationIntegrationEvent @event)
        {
            var coupon = await _couponRepository.FindCouponByCodeAsync(@event.CouponCode);

            if (coupon?.Consumed ?? true)
            {
                var unsuccessEvent = new OrderCouponRejectedIntegrationEvent
                {
                    CouponCode = @event.CouponCode,
                    OrderId = @event.OrderId
                };

                _eventBus.Publish(unsuccessEvent);
            }
            else
            {
                coupon.OrderId = @event.OrderId;
                coupon.Consumed = true;
                await _couponRepository.UpdateAsync(coupon);
                var successEvent = new OrderCouponConfirmedIntegrationEvent
                {
                    CouponCode = @event.CouponCode,
                    OrderId = @event.OrderId
                };

                _eventBus.Publish(successEvent);
            }
        }
    }
}
