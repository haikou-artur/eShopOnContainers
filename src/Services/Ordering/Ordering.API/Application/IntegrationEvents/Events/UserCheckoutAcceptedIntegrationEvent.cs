﻿namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events;

public record UserCheckoutAcceptedIntegrationEvent : IntegrationEvent
{        
    public string UserId { get; }
        
    public string UserName { get; }
        
    public string City { get; set; }
        
    public string Street { get; set; }
        
    public string State { get; set; }

    public string Country { get; set; }

    public string ZipCode { get; set; }

    public string CardNumber { get; set; }

    public string CardHolderName { get; set; }

    public DateTime CardExpiration { get; set; }

    public string CardSecurityNumber { get; set; }

    public int CardTypeId { get; set; }

    public string Buyer { get; set; }

    public Guid RequestId { get; set; }

    public CustomerBasket Basket { get; }

    public string DiscountCode { get; set; }

    public decimal Discount { get; set; }

    public decimal Points { get; set; }

    public UserCheckoutAcceptedIntegrationEvent(string userId, string userName, string city, string street,
        string state, string country, string zipCode, string cardNumber, string cardHolderName,
        DateTime cardExpiration, string cardSecurityNumber, int cardTypeId, string buyer, Guid requestId,
        CustomerBasket basket, string discountCode, decimal discount, decimal points)
    {
        UserId = userId;
        City = city;
        Street = street;
        State = state;
        Country = country;
        ZipCode = zipCode;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        CardExpiration = cardExpiration;
        CardSecurityNumber = cardSecurityNumber;
        CardTypeId = cardTypeId;
        Buyer = buyer;
        Basket = basket;
        RequestId = requestId;
        UserName = userName;
        DiscountCode = discountCode;
        Discount = discount;
        Points = points;
    }

}
