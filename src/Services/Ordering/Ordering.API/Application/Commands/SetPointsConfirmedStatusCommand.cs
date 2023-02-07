namespace Ordering.API.Application.Commands
{
    public class SetPointsConfirmedStatusCommand : IRequest<bool>
    {
        public int OrderId { get; set; }

        public SetPointsConfirmedStatusCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
