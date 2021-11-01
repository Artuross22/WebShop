using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
using WebShop.ViewModels;
using System.Data.Entity;

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
        public ActionResult CreateDelivery(DeliveryInputModel model)  
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

        [HttpGet]
        public ActionResult Edit(int? deliveryId)
        {
            using(var db = new ShopContext())
            {
                if (deliveryId == null)
                    return HttpNotFound();

                var deliveri = db.Deliveries.Find(deliveryId);
                if (deliveri == null)
                    return HttpNotFound();

                var editDelivery = new DeliveryInputModel();
                InitializeDeliveryViewModel(editDelivery, deliveri);
                editDelivery.Id = deliveri.Id;

                return View(editDelivery);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // ще раз повтори для  чого?)
        public ActionResult Edit(DeliveryInputModel model )
        {
            if (!ModelState.IsValid)
                return View(model);
            using (var db = new  ShopContext())
            { 
                var delivery = ConvertToDeliveriVievModel(model);
                db.Entry(delivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Delivery");
            }
        }

        protected Delivery ConvertToDeliveriVievModel(DeliveryInputModel model)
        {
            var delivery = new Delivery
            {
                Id = model.Id,
                Name = model.Name
            };
            return delivery;
        }

        public void InitializeDeliveryViewModel<T>(T modul, Delivery delivery )
              where T : DeliveryInputModel
        {
            modul.Name = delivery.Name;         
        }





    }
}