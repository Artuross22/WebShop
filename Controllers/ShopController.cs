using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    public class ShopController : Controller
    {
        protected ShopContext ShopContext
        {
            get
            {
                return new ShopContext();
            }
        }

        [HttpGet]
        public ActionResult Categories()
        {
            var categories = ShopContext.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CategoryInputModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category();
                category.Name = model.Name;

                var db = new ShopContext();
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(model);
        }

        public ActionResult DeleteCategory(int categoryId)
        {
            var db = new ShopContext();

            var category = db.Categories.Find(categoryId);
            if (category == null)
                return RedirectToAction("Categories");

            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
    }
}