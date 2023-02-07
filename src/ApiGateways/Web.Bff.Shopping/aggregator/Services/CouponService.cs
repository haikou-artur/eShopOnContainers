namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlsConfig _config;

        public CouponService(HttpClient httpClient, IOptions<UrlsConfig> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }
        public async Task<Coupon> GetCouponByCodeAsync(string code)
        {
            var url = $"{_config.Coupons}{UrlsConfig.CouponOperations.GetCouponByCode(code)}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new Coupon();
            }

            var ordersDraftResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Coupon>(ordersDraftResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
