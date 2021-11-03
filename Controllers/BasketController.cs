using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Api;
using WebShop.DomainModels;
using WebShop.ViewModels.Basket;

namespace WebShop.Controllers
{
    public class BasketController : Controller
    {
        protected ShopContext ShopContext
        {
            get
            {
                return new ShopContext();
            }
        }

        public ActionResult Basket()
        {
            var basket = ApplicationContext.BasketApi.GetCurrentBasket(HttpContext);
            basket.BasketLines = LoadBasketLines(basket.Id);
            return View(basket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToBasket(AddProductToBasketModel model)
        {
            // 1. Створити баскет-лінію і заповнити її
            // 2. Отримати поточний баскет
            // 3. Зберегти баскет-лінію в поточний баскет

            var basketLine = new BasketLine
            {
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };

            ApplicationContext.BasketApi.AddToBasket(basketLine, HttpContext);
            return RedirectToAction("Basket");
        }

        protected List<BasketLine> LoadBasketLines(int basketId)
        {
            using(var db = new ShopContext())
            {
                var basketLines = db.BasketLines.Where(l => l.BasketId == basketId).ToList();
                foreach(var line in basketLines)
                {
                    line.Product = db.Products.Find(line.ProductId);
                }
                return basketLines;
            }
        }
    }
}