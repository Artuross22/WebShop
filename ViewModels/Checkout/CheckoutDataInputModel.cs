using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.ViewModels.Client;

namespace WebShop.ViewModels.Checkout
{
    public class CheckoutDataInputModel
    {
        public Address ClientAddress { get; set; }

        public ClientData ClientData { get; set; }

        public int DeliveryId { get; set; }

        public int BasketId { get; set; }
    }
}