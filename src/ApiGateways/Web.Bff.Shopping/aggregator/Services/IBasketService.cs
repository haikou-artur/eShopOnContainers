﻿namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Services;

public interface IBasketService
{
    Task<BasketData> GetByIdAsync(string id);

    Task UpdateAsync(BasketData currentBasket);
}

public interface IBasketHttpService
{
    Task CheckoutAsync(BasketCheckout basketCheckout, string requestId);
}
