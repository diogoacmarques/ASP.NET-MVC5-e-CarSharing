namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class brandupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Types", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Brands", "TypeId", "dbo.Brands");
            DropIndex("dbo.Types", new[] { "BrandId" });
            AddForeignKey("dbo.Brands", "TypeId", "dbo.Types", "TypeId", cascadeDelete: true);
            DropColumn("dbo.Types", "BrandId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Types", "BrandId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Brands", "TypeId", "dbo.Types");
            CreateIndex("dbo.Types", "BrandId");
            AddForeignKey("dbo.Brands", "TypeId", "dbo.Brands", "BrandId");
            AddForeignKey("dbo.Types", "BrandId", "dbo.Brands", "BrandId", cascadeDelete: true);
        }
    }
}
