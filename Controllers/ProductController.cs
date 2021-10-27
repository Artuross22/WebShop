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

        public ActionResult Index(string query)
        {
            query = PrepareSeachQuery(query);
            
            using(var db = new ShopContext())
            {
                var products = string.IsNullOrEmpty(query) ? db.Products.ToList() : db.Products.Where(p => p.Name.Contains(query)).ToList();

                foreach(var product in products)
                {
                    product.Category = db.Categories.Find(product.CategoryId);
                }

                return View(products);
            }
        }

        protected List<Product> Search()
        {

            return null;
        }

        public ActionResult Details(int? id)
        {
            using(var db = new ShopContext())
            {
                var product = db.Products.Find(id);
                if (product == null)
                    return HttpNotFound();

                var productViewModel = new ProductDetailsViewModel();
                InitializeProductDetailsViewModel(productViewModel, product);

                return View(productViewModel);
            }
        }

        protected void InitializeProductDetailsViewModel(ProductDetailsViewModel model, Product product)
        {
            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.Producer = product.Producer;

            using(var db = new ShopContext())
            {
                model.Category = db.Categories.Find(product.CategoryId);
            }
        }

        private string PrepareSeachQuery(string query)
        {
            return query?.Trim().ToLower();
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
                product.Description = model.Description;
                product.Price = model.Price;
                product.Producer = model.Producer;
                product.CategoryId = model.CategoryId;

                using(var db = new ShopContext())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult DeleteProducts(int productId)
        {
            var db = new ShopContext();

            var product = db.Products.Find(productId);
            if (product == null)
                return RedirectToAction("Products");
            
            db.Products.Remove(product);
            db.SaveChanges();
           return RedirectToAction("Index");
        }


    }
}