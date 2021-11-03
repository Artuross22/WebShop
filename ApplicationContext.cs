using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Api;

namespace WebShop
{
    public class ApplicationContext
    {
        public static HttpContextBase HttpContext { get; set; }

        public static BasketApi BasketApi
        {
            get
            {
                return new BasketApi();
            }
        }
    }
}