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
        public Basket GetCurrentBasket(bool loadBasketLines = false) // шо за хтт
        {
            var basketId = ApplicationContext.HttpContext.Request.Cookies.Get(Constants.Basket.BasketId); // получаємо кук
            if (basketId != null)          
            {
                using (var db = new ShopContext()) // визиваємо шоп контекст 
                {
                    var existingBasket = db.Baskets.Find(int.Parse(basketId.Value)); // переводим кук в инт ( по дефолту куки стрингові)
                    if (loadBasketLines)
                        existingBasket.BasketLines = LoadBasketLines(existingBasket.Id); // если что-то есть в  existingBasket.BasketLines то записую . якщо немає то
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

                var existingBasketLine = db.BasketLines.FirstOrDefault(l => l.BasketId == currentBasket.Id && l.ProductId == line.ProductId);
                if(existingBasketLine == null)
                    basket.BasketLines.Add(line);    // в нашу баскет лінію по айдишки (на даний момент 2)  добавляеться товар (продуктАйди і количество)
                else
                    existingBasketLine.Quantity += line.Quantity;

                db.SaveChanges();               // сохраняємо це все в нашій базі даних
            }
        }

        public void ClearCurrentBasket()
        {
            var basketId = ApplicationContext.HttpContext.Request.Cookies.Get(Constants.Basket.BasketId); // получаємо кук
            if (basketId != null)
            {
                basketId.Expires = DateTime.Now.AddSeconds(1);
                ApplicationContext.HttpContext.Response.Cookies.Add(basketId);
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

        protected List<BasketLine> LoadBasketLines(int basketId)
        {
            using (var db = new ShopContext())
            {
                var basketLines = db.BasketLines.Where(l => l.BasketId == basketId).ToList();
                foreach (var line in basketLines)
                {
                    line.Product = db.Products.Find(line.ProductId);
                }
                return basketLines;
            }
        }
    }
}