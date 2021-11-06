using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Api;

namespace WebShop
{
    public static class ApplicationContext
    {
        public static HttpContextBase HttpContext
        {
            get
            {
                return new HttpContextWrapper(System.Web.HttpContext.Current);
            }
        }

        public static BasketApi BasketApi
        {
            get
            {
                return new BasketApi();
            }
        }
    }
}