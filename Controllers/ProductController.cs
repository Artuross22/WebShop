using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        protected ShopContext ShopContext
        {
            get
            {
                return new ShopContext();
            }
        }

        public ActionResult Index()
        {
            var db = new ShopContext();

            var products = db.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(ShopContext.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductInputModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                product.Name = model.Name;

                var db = new ShopContext();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}