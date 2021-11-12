using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels.Client
{
    public class Address
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int HouseNumber { get; set; }

     
        public int? ApartmentNumber { get; set; }

        public override string ToString()
        {
            return string.Join(" | ", Country, City, Street, HouseNumber, ApartmentNumber);
        }
    }
}