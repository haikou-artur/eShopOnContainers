﻿namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services;

public class OrderApiClient : IOrderApiClient
{
    private readonly HttpClient _apiClient;
    private readonly ILogger<OrderApiClient> _logger;
    private readonly UrlsConfig _urls;

    public OrderApiClient(HttpClient httpClient, ILogger<OrderApiClient> logger, IOptions<UrlsConfig> config)
    {
        _apiClient = httpClient;
        _logger = logger;
        _urls = config.Value;
    }

    public async Task<Order> GetOrderAsync(int id)
    {
        var uri = $"{_urls.Orders}/api/v1/orders/{id}";
        var response = await _apiClient.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<Order>(content);
    }

    public async Task<OrderData> GetOrderDraftFromBasketAsync(BasketData basket)
    {
        var url = $"{_urls.Orders}{UrlsConfig.OrdersOperations.GetOrderDraft()}";
        var content = new StringContent(JsonSerializer.Serialize(basket), System.Text.Encoding.UTF8, "application/json");
        var response = await _apiClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();

        var ordersDraftResponse = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<OrderData>(ordersDraftResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<IEnumerable<OrderSummary>> GetOrderSummaryAsync()
    {
        var uri = $"{_urls.Orders}/api/v1/orders";
        var response = await _apiClient.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IEnumerable<OrderSummary>>(content);
    }
}
