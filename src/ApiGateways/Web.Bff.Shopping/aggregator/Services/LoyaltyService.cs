namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services
{
    public class LoyaltyService : ILoyaltyService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _config;

        public LoyaltyService(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }

        public async Task<decimal> GetLoyaltyAsync()
        {
            var url = $"{_config.Coupons}{UrlsConfig.LoyaltyOperations.GetLoyalty()}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            var ordersDraftResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<decimal>(ordersDraftResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
