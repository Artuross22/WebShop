using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.DomainModels;

namespace WebShop.Api
{
    public class BasketApi
    {
        public static Basket GetCurrentBasket(HttpContextBase httpContext)
        {
            var basketId = httpContext.Request.Cookies.Get(Constants.Basket.BasketId);
            if (basketId != null)
            {
                using (var db = new ShopContext())
                {
                    return db.Baskets.Find(int.Parse(basketId.Value));
                }
            }
            else
            {
                var initialBasket = CreateInitialBasket();
                var cookie = new HttpCookie(Constants.Basket.BasketId, initialBasket.Id.ToString())
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                httpContext.Response.Cookies.Add(cookie);
                return initialBasket;
            }
        }

        protected static Basket CreateInitialBasket()
        {
            using (var db = new ShopContext())
            {
                var basket = new Basket();
                db.Baskets.Add(basket);
                db.SaveChanges();

                return basket;
            }
        }
    }
}