using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebShop.ViewModels.Client;

namespace WebShop.ViewModels.Checkout
{
    public class CheckoutDataInputModel
    {
        [Required]
        public Address ClientAddress { get; set; }

        [Required]
        public ClientData ClientData { get; set; }

        [Required]
        public int DeliveryId { get; set; }

        [Required]
        public int BasketId { get; set; }

        public List<BasketLineModel> BasketLines { get; set; }
    }
}