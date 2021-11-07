using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.DomainModels
{
    public class Order
    {
        public int Id { get; set; }
        
        public decimal TotalPrice { get; set; }

        public int? DeliveryId { get; set; }

        public int? AddressId { get; set; }

        public Delivery Delivery { get; set; }

        public Address Address { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }
    }
}