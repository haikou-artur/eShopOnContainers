using Coupon.Domain.EventHandlers;
using Coupon.Domain.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Coupon.Infrastructure.Configuration
{
    public static class IApplicationBuildeExtensions
    {
        public static void UseEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderStatusChangedToAwaitingCouponValidationIntegrationEvent, OrderStatusChangedToAwaitingCouponValidationIntegrationEventHandler>();
        }
    }
}
