using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
using WebShop.ViewModels;

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
        public ActionResult CreateOrders()
        {
            ViewBag.Deliveries = new SelectList(ShopContext.Deliveries, "Id", "TotalPrice");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]   // ? против злому сайта ? // паролі і логіни 
        public ActionResult CreateOrders(OrderInputModel model) //?? щоб показувати на екран те що ми вибрали ? 
        {
            if (ModelState.IsValid)
            {
                var order = new Order();
                order.TotalPrice = model.TotalPrice;

                var db = new ShopContext();
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Orders");
            }
               return View(model);
        }





    }
}