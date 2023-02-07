namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Controllers;

[Route("o/api/v1/[controller]s")]
[Authorize]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly IOrderingService _orderingService;
    private readonly IOrderApiClient _orderApiClient;

    public OrderController(IBasketService basketService, IOrderingService orderingService, IOrderApiClient orderApiClient)
    {
        _basketService = basketService;
        _orderingService = orderingService;
        _orderApiClient = orderApiClient;
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<OrderSummary>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<OrderSummary>>> GetOrdersAsync()
    {
        var t = await _orderApiClient.GetOrderSummaryAsync();
        return Ok(t);
    }

    [Route("{orderId:int}")]
    [HttpGet]
    [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Order>> GetOrderAsync(int orderId)
    {
        try
        {
            //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
            //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
            return await _orderApiClient.GetOrderAsync(orderId);
        }
        catch
        {
            return NotFound();
        }
    }

    [Route("draft/{basketId}")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OrderData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderData>> GetOrderDraftAsync(string basketId)
    {
        if (string.IsNullOrWhiteSpace(basketId))
        {
            return BadRequest("Need a valid basketid");
        }
        // Get the basket data and build a order draft based on it
        var basket = await _basketService.GetByIdAsync(basketId);

        if (basket == null)
        {
            return BadRequest($"No basket found for id {basketId}");
        }

        return await _orderingService.GetOrderDraftAsync(basket);
    }
}
