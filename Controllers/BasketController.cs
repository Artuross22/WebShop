using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Api;
using WebShop.DomainModels;
using WebShop.ViewModels.Basket;

namespace WebShop.Controllers
{
    public class BasketController : Controller
    {
        protected ShopContext ShopContext
        {
            get
            {
                return new ShopContext();
            }
        }

        public ActionResult Basket()
        {
            using (var db = new ShopContext())
            {
                var basket = ApplicationContext.BasketApi.GetCurrentBasket(HttpContext);
                basket.BasketLines = LoadBasketLines(basket.Id);

                var viewModels = new BacketViewModel();
                InitializeBasketView(viewModels, basket);
                viewModels.Id = basket.Id;
                return View(viewModels);
            }
        }

        protected void InitializeBasketView<T>(T model, Basket basket)
            where T : BacketViewModel
        {
            model.Id = basket.Id;
            model.BasketLines = basket.BasketLines;
        }
            

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToBasket(AddProductToBasketModel model)  // Передається продукт(айди продукту) і кількість 
        {
          
            // 2. Отримати поточний баскет
            // 3. Зберегти баскет-лінію в поточний баскет

            var basketLine = new BasketLine  // Створити баскет-лінію і заповнити її . передаємо інфу
            {
                ProductId = model.ProductId, 
                Quantity = model.Quantity
            };

            ApplicationContext.BasketApi.AddToBasket(basketLine, HttpContext); //Создаємо лінію.  Викликаємо клас  BasketApi метод AddToBasket і передаємо йому (basketLine, HttpContext)
                                                                                 
            return RedirectToAction("Basket");                                 //и передаємо йому  = баскет лінію(продукт і кількість)  такоже HttpContext(инфу з нету (кук))
        }                                                                    // поки є конект з сервером буде відкритий доступ до HttpContext

        protected List<BasketLine> LoadBasketLines(int basketId)
        {
            using(var db = new ShopContext())
            {
                var basketLines = db.BasketLines.Where(l => l.BasketId == basketId).ToList();
                foreach(var line in basketLines)
                {
                    line.Product = db.Products.Find(line.ProductId);
                }
                return basketLines;
            }
        }

        public ActionResult DeleteBasket(int id)
        {
            using (var db = new ShopContext())
            {
                var backet = db.BasketLines.Find(id);
                if (backet == null)
                    return HttpNotFound();

                db.BasketLines.Remove(backet);
                db.SaveChanges();
                return RedirectToAction("Basket");

            }

           
        }
    }
}