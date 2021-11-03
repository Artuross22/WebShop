namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCategoryForeignKeyFromBasketLine : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BasketLines", "Category_Id", "dbo.Categories");
            DropIndex("dbo.BasketLines", new[] { "Category_Id" });
            DropColumn("dbo.BasketLines", "Category_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BasketLines", "Category_Id", c => c.Int());
            CreateIndex("dbo.BasketLines", "Category_Id");
            AddForeignKey("dbo.BasketLines", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
