using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.DomainModels
{
    public class OrderLine
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}