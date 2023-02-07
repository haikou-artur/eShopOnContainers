namespace Ordering.API.Application.Commands
{
    public class SetPointsRejectedStatusCommand : IRequest<bool>
    {
        public int OrderId { get; set; }

        public SetPointsRejectedStatusCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
