namespace Ordering.API.Application.Commands
{
    public class SetCouponRejectedStatusCommandHandler : IRequestHandler<SetCouponRejectedStatusCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public SetCouponRejectedStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(SetCouponRejectedStatusCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(10000, cancellationToken);

            var orderToUpdate = await _orderRepository.GetAsync(request.OrderNumber);
            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetRejectedCouponStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
