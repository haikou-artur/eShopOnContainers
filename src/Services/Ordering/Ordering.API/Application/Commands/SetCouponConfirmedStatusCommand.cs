namespace Ordering.API.Application.Commands
{
    public class SetCouponConfirmedStatusCommand : IRequest<bool>
    {
        [DataMember]
        public int OrderNumber { get; private set; }

        public SetCouponConfirmedStatusCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
