using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
    public class ProductInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Producer { get; set; }

        public int CategoryId { get; set; }
    }
}