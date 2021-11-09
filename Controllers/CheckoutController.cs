using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
using WebShop.ViewModels.Basket;
using WebShop.ViewModels.Checkout;

namespace WebShop.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (var db = new ShopContext())
            {
                var basket = ApplicationContext.BasketApi.GetCurrentBasket();
                basket.BasketLines = LoadBasketLines(basket.Id);

                var viewModels = new CheckoutDataInputModel();
                InitializeBasketView(viewModels, basket);
                viewModels.Id = basket.Id;

                ViewBag.Deliveries = new SelectList(db.Deliveries.ToList(), "Id", "Name");
                return View(viewModels);             
            }
        }
        protected void InitializeBasketView<T>(T model, Basket basket)
           where T : CheckoutDataInputModel
        {
            model.Id = basket.Id;
            model.BasketLines = basket.BasketLines;
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

        [HttpPost]
        public ActionResult Index(CheckoutDataInputModel model)
        {
            using (var db = new ShopContext())
            {
                ViewBag.Deliveries = new SelectList(db.Deliveries.ToList(), "Id", "Name");
                return View(model);
            }
        }
    }
}