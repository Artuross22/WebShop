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
                var result = new List<Product>();

                if (string.IsNullOrEmpty(query))
                {
                    result = db.Products.ToList();
                }
                else
                {
                    result = db.Products.Where(p => p.Name.Contains(query)
                        || p.Description.Contains(query)
                        || p.Category.Name.Contains(query)
                        || p.Producer.Contains(query)).ToList();
                }

                foreach (var product in result)
                {
                    product.Category = db.Categories.Find(product.CategoryId);
                }
                return result;
            }
        }

        public ActionResult Details(int? id)   // передаеться айди 37
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
        public ActionResult EditProduct(int? productId)  // передаю айди через Http
        {
            using (var db = new ShopContext())        // визиваю базу даних 
            {
                if (productId == null)      // провірка на те . чи таке айди є 
                {
                    return HttpNotFound(); // якщо немає вибивай нот фаунд
                }

                var product = db.Products.Find(productId); // по айдишки з бази даних витягуємо  информацию яка прикріплена до цього айди (адидас.ціна =10. и ткд)
                if (product == null)                      // якщо такої інформації немає / то нот фаунд
                {
                    return HttpNotFound();
                }

                var categories = db.Categories.ToList();      // передаємо сюди категорії (5)


                var editModel = new ProductEditInputModel();        // Беремо базові назви з якими нам потрібно працювати (нейм.дескрипшин и ткд.)
                InitializeProductViewModel(editModel, product);// передаємо в наш типа конструктор для ініціалізації . editModel = базові моделі які ми ініціалізуємо які прийдуть з product
                editModel.Id = product.Id; //  добавляю айди 
                editModel.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId); // по айди воно підтягує категорію.()  прикручуємо сюди категорію / спорт і айди категорії

                ViewBag.Categories = new SelectList(categories, "Id", "Name", editModel.CategoryId); // передаємо категорії які у  нас є (айди и имя категорії) і оприділену модель(все про бол) до категорії
                return View(editModel);
                //заполучаємо значення завдяки айди і повертаємо значення яке у нас було завдяки конструктору. і також даємо можливість вибрати категорію завдяки ViewBag.Categories = new SelectList
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductEditInputModel model)
        {
            if (!ModelState.IsValid)
                return View(model);  // дивиться чи моделі було щось поміняно. якщо нічо не було поміняно повертає назат модель якщо було проходить дальше

            using (var db = new ShopContext()) //база даних підтягуеться
            {
                var product = ConvertToProductDomainModel(model); // переформировка конструктора 
                db.Entry(product).State = EntityState.Modified;   // сохранити модификаци
                db.SaveChanges();     // сохроняем в базу даних
                return RedirectToAction("Index"); // повернути индекс
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

        protected void InitializeProductViewModel<T>(T model, Product product) // T model - тут стоїть дженерік який заміняє типізацію( стринг . инт. и ткд.) чому дженерик ?
            where T : ProductBaseViewModel              // можна працювати тільки з ProductBaseViewModel     
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

                using (var db = new ShopContext())
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