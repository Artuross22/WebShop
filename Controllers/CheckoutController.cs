using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
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
                ViewBag.Deliveries = new SelectList(db.Deliveries.ToList(), "Id", "Name");
                return View();
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