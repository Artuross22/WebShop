using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.DomainModels;
using WebShop.ViewModels;
using WebShop.ViewModels.Checkout;
using WebShop.ViewModels.Client;

namespace WebShop.Api
{
    public class OrderApi
    {
        public Order CreateOrder(CheckoutDataInputModel checkoutData, Basket basket)
        {
            var initialOrder = new Order
            {
                ClientData = checkoutData.ClientData.ToString(),
                ClientAddress = checkoutData.ClientAddress.ToString(),
                DeliveryId = checkoutData.DeliveryId
            };


            var order = SaveOrder(initialOrder);

            using (var db = new ShopContext())
            {
                var savedOrder = db.Orders.Find(order.Id);

                var orderLines = basket.BasketLines.Select(l => ConvertToOrderLine(l)).ToList();
                foreach(var orderLine in orderLines)
                {
                    savedOrder.OrderLines.Add(orderLine);
                }

                CalculateOrder(savedOrder);

                db.SaveChanges();
                ApplicationContext.BasketApi.ClearCurrentBasket();

                return savedOrder;
            }
        }

        protected OrderLine ConvertToOrderLine(BasketLine basketLine)
        {
            return new OrderLine { ProductId = basketLine.ProductId, Quantity = basketLine.Quantity, Product = basketLine.Product };
        }

        protected Order SaveOrder(Order order)
        {
            using (var db = new ShopContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();

                return order;
            }
        }

        public void CalculateOrder(Order order)
        {
            order.TotalPrice = order.OrderLines.Sum(l => l.Product.Price);
        }
    }
}