using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebShop.DomainModels;

namespace WebShop.ViewModels.Product
{
    public class ProductEditInputModel : ProductBaseViewModel
    {
        public int Id { get; set; }

        public Category Category { get; set; }
    }
}