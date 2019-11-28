namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.VehicleNumberPassengers", newName: "NumberPassengers");
            DropForeignKey("dbo.Vehicles", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Vehicles", "VehicleSeatId", "dbo.VehicleNumberPassengers");
            DropIndex("dbo.Vehicles", new[] { "CategoryId" });
            DropPrimaryKey("dbo.NumberPassengers");
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TypeId);
            
            AddColumn("dbo.Vehicles", "TypeId", c => c.Int());
            AddColumn("dbo.NumberPassengers", "NumberPassengersId", c => c.Int(nullable: false));
            AddColumn("dbo.NumberPassengers", "NumberOfPassengers", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.NumberPassengers", "NumberPassengersId");
            CreateIndex("dbo.Vehicles", "TypeId");
            AddForeignKey("dbo.Vehicles", "TypeId", "dbo.Types", "TypeId");
            AddForeignKey("dbo.Vehicles", "VehicleSeatId", "dbo.NumberPassengers", "NumberPassengersId");
            DropColumn("dbo.Vehicles", "CategoryId");
            DropColumn("dbo.NumberPassengers", "VehicleCarryId");
            DropColumn("dbo.NumberPassengers", "VehicleNumberOfPassengers");
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.NumberPassengers", "VehicleNumberOfPassengers", c => c.Int(nullable: false));
            AddColumn("dbo.NumberPassengers", "VehicleCarryId", c => c.Int(nullable: false));
            AddColumn("dbo.Vehicles", "CategoryId", c => c.Int());
            DropForeignKey("dbo.Vehicles", "VehicleSeatId", "dbo.NumberPassengers");
            DropForeignKey("dbo.Vehicles", "TypeId", "dbo.Types");
            DropIndex("dbo.Vehicles", new[] { "TypeId" });
            DropPrimaryKey("dbo.NumberPassengers");
            DropColumn("dbo.NumberPassengers", "NumberOfPassengers");
            DropColumn("dbo.NumberPassengers", "NumberPassengersId");
            DropColumn("dbo.Vehicles", "TypeId");
            DropTable("dbo.Types");
            AddPrimaryKey("dbo.NumberPassengers", "VehicleCarryId");
            CreateIndex("dbo.Vehicles", "CategoryId");
            AddForeignKey("dbo.Vehicles", "VehicleSeatId", "dbo.VehicleNumberPassengers", "VehicleCarryId");
            AddForeignKey("dbo.Vehicles", "CategoryId", "dbo.Categories", "CategoryId");
            RenameTable(name: "dbo.NumberPassengers", newName: "VehicleNumberPassengers");
        }
    }
}
