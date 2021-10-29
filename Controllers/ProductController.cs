using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
using WebShop.ViewModels.Product;

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
            var products = Search(query);
            return View(products);

        }

        protected List<Product> Search(string query)
        {
            using (var db = new ShopContext())
            {
                var search = string.IsNullOrEmpty(query) ? db.Products.ToList() : db.Products.Where(p => p.Name.Contains(query) || p.Description.Contains(query) || p.Producer.Contains(query)).ToList();             
                foreach (var product in search)
                {
                    product.Category = db.Categories.Find(product.CategoryId);
                }
                return search;
            }
        }
     

        public ActionResult Details(int? id)
        {
            using (var db = new ShopContext())
            {
                var product = db.Products.Find(id);
                if (product == null)
                    return HttpNotFound();

                var productViewModel = new ProductDetailsViewModel();
                InitializeProductViewModel(productViewModel, product);
                productViewModel.Id = product.Id;
                productViewModel.Category = db.Categories.Find(product.CategoryId);

                return View(productViewModel);
            }
        }

        [HttpGet]
        public ActionResult EditProduct(int? productId)
        {
            using (var db = new ShopContext())
            {
                if (productId == null)
                {
                    return HttpNotFound();
                }

                var product = db.Products.Find(productId);
                if (product == null)
                {
                    return HttpNotFound();
                }

                var categories = db.Categories.ToList();

                var editModel = new ProductEditInputModel();
                InitializeProductViewModel(editModel, product);
                editModel.Id = product.Id;
                editModel.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);

                ViewBag.Categories = new SelectList(categories, "Id", "Name", editModel.CategoryId);
                return View(editModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductEditInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var db = new ShopContext())
            {
                var product = ConvertToProductDomainModel(model);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        protected Product ConvertToProductDomainModel(ProductEditInputModel model)
        {
            var product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Producer = model.Producer,
                CategoryId = model.CategoryId
            };

            return product;
        }

        protected void InitializeProductViewModel<T>(T model, Product product)
            where T : ProductBaseViewModel
        {
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.Producer = product.Producer;
            model.CategoryId = product.CategoryId;
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