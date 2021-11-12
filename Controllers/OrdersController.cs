using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Api;
using WebShop.DomainModels;
using WebShop.ViewModels;
using WebShop.ViewModels.Checkout;
using WebShop.ViewModels.Client;

namespace WebShop.Controllers
{
    public class OrdersController : Controller
    {
        protected ShopContext ShopContext
        {
            get => new ShopContext();
        }

        [HttpGet]
        public ActionResult Orders()
        {
            ShopContext db = new ShopContext();

            var orderes = db.Orders.ToList();
            return View(orderes);

        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Deliveries = new SelectList(ShopContext.Deliveries, "Id", "Name");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderInputModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order();
                order.TotalPrice = model.TotalPrice;

                using (var db = new ShopContext())
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Orders");
            }
            return View(model);
        }

        public ActionResult PlaceOrder()
        {
            var checkoutData = TempData["checkoutData"] as CheckoutDataInputModel;
            if (checkoutData == null)
                return HttpNotFound();

            var basket = ApplicationContext.BasketApi.GetCurrentBasket(true);
            if (basket == null)
                return HttpNotFound();

            var order = ApplicationContext.OrderApi.CreateOrder(checkoutData, basket);

            return RedirectToAction("OrderSaved", new { orderId = order.Id });
        }

        public ActionResult OrderSaved(int? orderId)
        {
            if (orderId == null)
                return HttpNotFound();

            using (var db = new ShopContext())
            {
                var order = db.Orders.Find(orderId);
                if (order == null)
                    return HttpNotFound();

                return View("OrderSaved", order);
            }
        }
    }
}