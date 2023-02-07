namespace Ordering.API.Application.Commands
{
    public class SetPointsConfirmedStatusCommandHandler : IRequestHandler<SetPointsConfirmedStatusCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public SetPointsConfirmedStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(SetPointsConfirmedStatusCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(10000, cancellationToken);

            var order = await _orderRepository.GetAsync(request.OrderId);
            if (order == null)
            {
                return false;
            }

            order.SetPointsConfirmedStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
