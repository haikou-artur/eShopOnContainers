using System.Net.Http;
using System.Net.Http.Json;

namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services;

public class BasketService : IBasketService, IBasketHttpService
{
    private readonly Basket.BasketClient _basketClient;
    private readonly ILogger<BasketService> _logger;
    private readonly HttpClient _httpClient;
    private readonly UrlsConfig _urlsConfig;

    public BasketService(Basket.BasketClient basketClient, ILogger<BasketService> logger, HttpClient httpClient, IOptions<UrlsConfig> config)
    {
        _basketClient = basketClient;
        _logger = logger;
        _httpClient = httpClient;
        _urlsConfig = config.Value;
    }

    public async Task CheckoutAsync(BasketCheckout basketCheckout, string requestId)
    {
        var uri = $"{_urlsConfig.Basket}/api/v1/basket/checkout";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = JsonContent.Create(basketCheckout);
        request.Headers.Add("x-requestid", requestId);
        var reposne = await _httpClient.SendAsync(request);

        if (!reposne.IsSuccessStatusCode)
        {
            throw new Exception("Basket exeception");
        }
    }

    public async Task<BasketData> GetByIdAsync(string id)
    {
        _logger.LogDebug("grpc client created, request = {@id}", id);
        var response = await _basketClient.GetBasketByIdAsync(new BasketRequest { Id = id });
        _logger.LogDebug("grpc response {@response}", response);

        return MapToBasketData(response);
    }

    public async Task UpdateAsync(BasketData currentBasket)
    {
        _logger.LogDebug("Grpc update basket currentBasket {@currentBasket}", currentBasket);
        var request = MapToCustomerBasketRequest(currentBasket);
        _logger.LogDebug("Grpc update basket request {@request}", request);

        await _basketClient.UpdateBasketAsync(request);
    }

    private BasketData MapToBasketData(CustomerBasketResponse customerBasketRequest)
    {
        if (customerBasketRequest == null)
        {
            return null;
        }

        var map = new BasketData
        {
            BuyerId = customerBasketRequest.Buyerid
        };

        customerBasketRequest.Items.ToList().ForEach(item =>
        {
            if (item.Id != null)
            {
                map.Items.Add(new BasketDataItem
                {
                    Id = item.Id,
                    OldUnitPrice = (decimal)item.Oldunitprice,
                    PictureUrl = item.Pictureurl,
                    ProductId = item.Productid,
                    ProductName = item.Productname,
                    Quantity = item.Quantity,
                    UnitPrice = (decimal)item.Unitprice
                });
            }
        });

        return map;
    }

    private CustomerBasketRequest MapToCustomerBasketRequest(BasketData basketData)
    {
        if (basketData == null)
        {
            return null;
        }

        var map = new CustomerBasketRequest
        {
            Buyerid = basketData.BuyerId
        };

        basketData.Items.ToList().ForEach(item =>
        {
            if (item.Id != null)
            {
                map.Items.Add(new BasketItemResponse
                {
                    Id = item.Id,
                    Oldunitprice = (double)item.OldUnitPrice,
                    Pictureurl = item.PictureUrl,
                    Productid = item.ProductId,
                    Productname = item.ProductName,
                    Quantity = item.Quantity,
                    Unitprice = (double)item.UnitPrice
                });
            }
        });

        return map;
    }


}
