namespace Ordering.API.Application.Commands
{
    public class SetCouponRejectedStatusCommand : IRequest<bool>
    {
        [DataMember]
        public int OrderNumber { get; private set; }

        public SetCouponRejectedStatusCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
