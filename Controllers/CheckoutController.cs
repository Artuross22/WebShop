using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
using WebShop.ViewModels;
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
                var basket = ApplicationContext.BasketApi.GetCurrentBasket(true);

                var viewModel = new CheckoutDataInputModel();
                InitializeBasketView(viewModel, basket);

                ViewBag.Deliveries = new SelectList(db.Deliveries.ToList(), "Id", "Name");
                return View(viewModel);
            }
        }

        protected void InitializeBasketView<T>(T model, Basket basket)
           where T : CheckoutDataInputModel
        {
            model.BasketLines = new List<BasketLineModel>();
            foreach(var line in basket.BasketLines)
            {
                model.BasketLines.Add(new BasketLineModel
                {
                    Id = line.Id,
                    ProductId = line.ProductId,
                    Quantity = line.Quantity,
                    BasketId = line.BasketId,
                    Product = line.Product,
                    Basket = line.Basket,
                    TotalPrice = line.Product.Price * line.Quantity
                });
            }
        }

        [HttpPost]
        public ActionResult Index(CheckoutDataInputModel model)
        {
            using (var db = new ShopContext())
            {
                if (!ModelState.IsValid)
                {
                    InitializeBasketView(model, ApplicationContext.BasketApi.GetCurrentBasket(true));
                    ViewBag.Deliveries = new SelectList(db.Deliveries.ToList(), "Id", "Name");
                    return View(model);
                }

                return RedirectToAction("PlaceOrder", "Orders", model);
            }
        }
    }
}