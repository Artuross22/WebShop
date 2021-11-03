using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using WebShop.DomainModels;

namespace WebShop.Api
{
    public class BasketApi
    {
        public static Basket GetCurrentBasket(HttpContextBase httpContext) // шо за хттп контекст ?
        {
            var basketId = httpContext.Request.Cookies.Get(Constants.Basket.BasketId); // получаємо кук
            if (basketId != null)          
            {
                using (var db = new ShopContext())
                {
                    var existingBasket = db.Baskets.Find(int.Parse(basketId.Value));
                    existingBasket.BasketLines = existingBasket.BasketLines ?? new List<BasketLine>();
                    return existingBasket;
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