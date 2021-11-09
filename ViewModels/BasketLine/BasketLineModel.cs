using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
    public class BasketLineModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int? BasketId { get; set; }

        public decimal TotalPrice { get; set; }

        public DomainModels.Basket Basket { get; set; }

        public DomainModels.Product Product { get; set; }
    }
}