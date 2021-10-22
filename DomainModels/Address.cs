using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.DomainModels
{
    public class Address
    {
        public int Id { get; set; }

        public string  Postcode { get; set; }

        public string Contry { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set;}

        public ICollection<Order> Orders { get; set; }

    }
}