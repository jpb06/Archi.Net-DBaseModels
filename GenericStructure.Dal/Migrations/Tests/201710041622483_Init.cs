namespace GenericStructure.Dal.Migrations.Tests
{
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCategory = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false, maxLength: 2048),
                        ImagesPath = c.Guid(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.IdCategory, cascadeDelete: true)
                .Index(t => t.IdCategory)
                .Index(t => t.ImagesPath, unique: true);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOrder = c.Int(nullable: false),
                        IdArticle = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitCost = c.Decimal(nullable: false, storeType: "money"),
                        LineItemTotal = c.Decimal(storeType: "money"),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.IdArticle, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.IdOrder, cascadeDelete: true)
                .Index(t => t.IdOrder)
                .Index(t => t.IdArticle);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCustomer = c.Int(nullable: false),
                        Reference = c.String(nullable: false, maxLength: 256),
                        OrderDate = c.DateTime(nullable: false),
                        ShipDate = c.DateTime(nullable: false),
                        PaymentCardMember = c.String(nullable: false, maxLength: 20),
                        PaymentCardExpiration = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.IdCustomer, cascadeDelete: true)
                .Index(t => t.IdCustomer);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false, maxLength: 128),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "IdOrder", "dbo.Orders");
            DropForeignKey("dbo.Orders", "IdCustomer", "dbo.Customers");
            DropForeignKey("dbo.OrderDetails", "IdArticle", "dbo.Articles");
            DropForeignKey("dbo.Articles", "IdCategory", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "IdCustomer" });
            DropIndex("dbo.OrderDetails", new[] { "IdArticle" });
            DropIndex("dbo.OrderDetails", new[] { "IdOrder" });
            DropIndex("dbo.Articles", new[] { "ImagesPath" });
            DropIndex("dbo.Articles", new[] { "IdCategory" });
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Categories");
            DropTable("dbo.Articles");
        }
    }
}
