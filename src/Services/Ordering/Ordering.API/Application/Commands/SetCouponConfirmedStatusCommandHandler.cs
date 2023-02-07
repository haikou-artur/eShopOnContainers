using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ordering.API.Application.Commands
{
    public class SetCouponConfirmedStatusCommandHandler : IRequestHandler<SetCouponConfirmedStatusCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public SetCouponConfirmedStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(SetCouponConfirmedStatusCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(10000, cancellationToken);

            var orderToUpdate = await _orderRepository.GetAsync(request.OrderNumber);
            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetConfirmedCouponStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
