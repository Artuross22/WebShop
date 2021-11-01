using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.DomainModels
{
    public class Basket
    {
        public int Id { get; set; }

        public ICollection<BasketLine> BasketLines { get; set; }

        public Basket()
        {
            BasketLines = new List<BasketLine>();
        }
    }
}