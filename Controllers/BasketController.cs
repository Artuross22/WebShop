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
            var basket = BasketApi.GetCurrentBasket(HttpContext);                // метод для отримання  айди(кука) . назва через константу Constants
            basket.BasketLines = basket.BasketLines ?? new List<BasketLine>();  // що тут відбувається ?
            return View(basket);
        }

        [HttpGet]
        public ActionResult AddToBasket()
        {
           
            
        }


        public ActionResult AddToBasket(AddProductToBasketModel model)
        {

            // please implement this method and configure redirect to Basket page
            return RedirectToAction("Details", "Product");
        }
    }
}