namespace finalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lat = c.Double(nullable: false),
                        Long = c.Double(nullable: false),
                        City = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 30),
                        Telephone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purchase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Branch", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UserId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Size = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        PictureName = c.String(),
                        SupplierId = c.Int(nullable: false),
                        ProductTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductType", t => t.ProductTypeId)
                .ForeignKey("dbo.Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        PictureName = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 30),
                        Gender = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Telephone = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Purchase", "BranchId", "dbo.Branch");
            DropForeignKey("dbo.Purchase", "UserId", "dbo.User");
            DropForeignKey("dbo.Product", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.Purchase", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "ProductTypeId", "dbo.ProductType");
            DropIndex("dbo.Product", new[] { "ProductTypeId" });
            DropIndex("dbo.Product", new[] { "SupplierId" });
            DropIndex("dbo.Purchase", new[] { "BranchId" });
            DropIndex("dbo.Purchase", new[] { "UserId" });
            DropIndex("dbo.Purchase", new[] { "ProductId" });
            DropTable("dbo.User");
            DropTable("dbo.Supplier");
            DropTable("dbo.ProductType");
            DropTable("dbo.Product");
            DropTable("dbo.Purchase");
            DropTable("dbo.Branch");
        }
    }
}
