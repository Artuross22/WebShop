using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.DomainModels;

namespace WebShop.ViewModels.Basket
{
    public class BacketViewModel
    {
        public int Id { get; set; }
        public ICollection<BasketLine> BasketLines { get; set; }
    }
}