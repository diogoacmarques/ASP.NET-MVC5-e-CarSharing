namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rents", "OwnerID", "dbo.AspNetUsers");
            DropIndex("dbo.Rents", new[] { "OwnerID" });
            DropColumn("dbo.Vehicles", "VehiclePlate");
            DropColumn("dbo.Rents", "OwnerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rents", "OwnerID", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Vehicles", "VehiclePlate", c => c.String());
            CreateIndex("dbo.Rents", "OwnerID");
            AddForeignKey("dbo.Rents", "OwnerID", "dbo.AspNetUsers", "Id");
        }
    }
}
