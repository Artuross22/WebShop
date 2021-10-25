using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
    public class OrderInputModel
    {

        public decimal TotalPrice { get; set; }

        public int?  DeliveryId { get; set; }

        public int? AddressId { get; set; }
    }
}