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
    }
}