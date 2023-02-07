﻿namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Models
{
    public class BasketCheckout
    {
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

        public string DiscountCode { get; set; }

        public decimal Discount { get; set; }

        public decimal Points { get; set; }
    }
}
