using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.DomainModels;
using WebShop.ViewModels.BasketLine;

namespace WebShop.Controllers
{
    public class BasketLineController : Controller
    { 
        protected ShopContext ShopContext
        {
            get
            {
                return new ShopContext();
            }        
        }

        [HttpGet]
        public ActionResult BasketLine()
        {
            var basketLine = ShopContext.BasketLines.ToList();
            return View(basketLine);
        }

        [HttpGet]
        public ActionResult CreateBasketLine()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBasketLine(BasketLineInputModel model)
        {
          if(ModelState.IsValid)
            {
                var basketLine = new BasketLine();
                basketLine.Quantity = model.Quantity;
                basketLine.Quantity = model.ProductId;
                basketLine.BasketId = model.BasketId;

                using (var db = new ShopContext())
                {
                    db.BasketLines.Add(basketLine);
                    db.SaveChanges();
                }
                return RedirectToAction("BasketLine");
          }
            return View(model);
        }




    }
}