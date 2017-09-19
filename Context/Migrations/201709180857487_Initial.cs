namespace Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            //Sql("create view dbo.CategoryBalances ...");
            //Sql("create trigger ProductTrigger ...");
            //CreateTable(
            //    "dbo.CategoryBalances",
            //    c => new
            //        {
            //            Category = c.String(nullable: false, maxLength: 32),
            //            Quantity = c.Int(nullable: false),
            //            Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        })
            //    .PrimaryKey(t => t.Category);

            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(maxLength: 32),
                        StockId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 32, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(nullable: false, maxLength: 32, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "StockId", "dbo.Stocks");
            DropIndex("dbo.Products", new[] { "StockId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.Products");            

            //DropTable("dbo.CategoryBalances");
            //Sql("drop trigger ProductTrigger");
            //Sql("drop view dbo.CategoryBalances");
        }
    }
}
