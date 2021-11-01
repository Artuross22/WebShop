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
        public ActionResult Basket()
        {
            var basket = BasketApi.GetCurrentBasket(HttpContext);
            return View(basket);
        }

        public ActionResult AddToBasket(AddProductToBasketModel model)
        {
            var basketLine = new BasketLine
            {
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };

            var currentBasket = BasketApi.GetCurrentBasket(HttpContext);
            using(var db = new ShopContext())
            {
                var basket = db.Baskets.Find(currentBasket.Id);
                basket.BasketLines.Add(basketLine);
                db.SaveChanges();
            }

            return RedirectToAction("Basket");
        }
    }
}