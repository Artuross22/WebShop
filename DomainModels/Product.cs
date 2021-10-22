using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.DomainModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Producer { get; set; }


        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        //public ICollection<OrderLine> OrderLines { get; set; }

        //public ICollection<BasketLine> BasketLines { get; set; }
    }
}