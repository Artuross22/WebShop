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
        public Basket GetCurrentBasket() // шо за хтт
        {
            var basketId = ApplicationContext.HttpContext.Request.Cookies.Get(Constants.Basket.BasketId); // получаємо кук
            if (basketId != null)          
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
                ApplicationContext.HttpContext.Response.Cookies.Add(cookie);
                return initialBasket;
            }
        }

        public void AddToBasket(BasketLine line)
        {
            var currentBasket = GetCurrentBasket();

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