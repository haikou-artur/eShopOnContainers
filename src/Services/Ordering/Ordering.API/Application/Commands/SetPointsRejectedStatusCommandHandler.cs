namespace Ordering.API.Application.Commands
{
    public class SetPointsRejectedStatusCommandHandler : IRequestHandler<SetPointsRejectedStatusCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public SetPointsRejectedStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(SetPointsRejectedStatusCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(10000, cancellationToken);

            var order = await _orderRepository.GetAsync(request.OrderId);
            if (order == null)
            {
                return false;
            }

            order.SetRejectedPointsStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
