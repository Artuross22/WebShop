using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebShop.DomainModels
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("ShopDb") { }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketLine> BasketLines { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}