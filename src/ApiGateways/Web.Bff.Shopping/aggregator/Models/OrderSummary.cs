﻿namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Models
{
    public class OrderSummary
    {
        public int ordernumber { get; init; }
        public DateTime date { get; init; }
        public string status { get; init; }
        public double total { get; init; }
        public string discountcode { get; init; }
        public decimal discount { get; init; }
        public double subtotal { get; init; }
        public decimal points { get; init; }
    }

    public class Order
    {
        public int ordernumber { get; init; }
        public DateTime date { get; init; }
        public string status { get; init; }
        public string description { get; init; }
        public string street { get; init; }
        public string city { get; init; }
        public string zipcode { get; init; }
        public string country { get; init; }
        public List<Orderitem> orderitems { get; set; }
        public decimal total { get; set; }
        public string discountcode { get; init; }
        public decimal discount { get; init; }
        public decimal subtotal { get; init; }
        public decimal points { get; init; }
    }

    public class Orderitem
    {
        public string productname { get; init; }
        public int units { get; init; }
        public double unitprice { get; init; }
        public string pictureurl { get; init; }
    }
}