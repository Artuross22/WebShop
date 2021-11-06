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
        //
        public Basket GetCurrentBasket(HttpContextBase httpContext) // информация з інтернету наш (кук)
        {
            var basketId = httpContext.Request.Cookies.Get(Constants.Basket.BasketId); // получаємо кук
            if (basketId != null)     // якщо кук є . то попадаємо в юзинг     
            {
                using (var db = new ShopContext()) // визиваємо шоп контекст 
                {
                    var existingBasket = db.Baskets.Find(int.Parse(basketId.Value)); // переводим кук в инт ( по дефолту куки стрингові)
                    existingBasket.BasketLines = existingBasket.BasketLines ?? new List<BasketLine>();  // если что-то есть в  existingBasket.BasketLines то записую . якщо немає то
                                                                                                       // створю нову баскет линию new List<BasketLine>(); 
                    return existingBasket;          // повертаємо наш кук                                              
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

        public void AddToBasket(BasketLine line, HttpContextBase httpContext) // приходить наша лінія і інфа з нету(кук)
        {
            var currentBasket = GetCurrentBasket(httpContext); // визиваємо метод для отрмання кука(айди)

            using (var db = new ShopContext())
            {
                var basket = db.Baskets.Find(currentBasket.Id); // отримуємо всі товари які під цим айди ?  
                basket.BasketLines.Add(line);    // в нашу баскет лінію по айдишки (на даний момент 2)  добавляеться товар (продуктАйди і количество)
                db.SaveChanges();               // сохраняємо це все в нашій базі даних
            }
        }

        protected Basket CreateInitialBasket()
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