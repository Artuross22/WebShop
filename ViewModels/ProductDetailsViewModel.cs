using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.DomainModels;

namespace WebShop.ViewModels
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Producer { get; set; }

        public Category Category { get; set; }
    }
}