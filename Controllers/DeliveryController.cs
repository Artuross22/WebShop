using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    public class DeliveryController : Controller 
    {
        protected ShopContext ShopContext
        {
            get => new ShopContext();
        }
        
        [HttpGet]
        public ActionResult Delivery()
        {
            var deliveries = ShopContext.Deliveries.ToList();
            return View(deliveries);
        }

        [HttpGet]
        public ActionResult CreateDelivery()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDelivery(DeliveryInputModel model)  // ???
        {
            if (ModelState.IsValid)
            {
                var delivery = new Delivery();
                delivery.Name = model.Name;

                var db = new ShopContext();
                db.Deliveries.Add(delivery);
                db.SaveChanges();
                return RedirectToAction("Delivery");
            }
            return View(model);
        }
         public ActionResult DeleteDelivery(int deliveryId)
         {
            var db = new ShopContext();

            var delivery = db.Deliveries.Find(deliveryId);
            if (delivery == null)
                return RedirectToAction("Delivery");
            db.Deliveries.Remove(delivery);
            db.SaveChanges();
            return RedirectToAction("Delivery");



        }






    }
}