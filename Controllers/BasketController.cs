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
            // please implement this method and configure redirect to Basket page
            return RedirectToAction("Details", "Product");
        }
    }
}