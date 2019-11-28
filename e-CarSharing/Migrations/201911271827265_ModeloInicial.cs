namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeloInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(nullable: false),
                        BrandId = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ModelId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        VehiclePlate = c.String(),
                        BrandId = c.Int(),
                        ModelId = c.Int(),
                        CategoryId = c.Int(),
                        ColourId = c.Int(),
                        VehicleSeatId = c.Int(),
                        Area = c.String(),
                        UserId = c.String(maxLength: 128),
                        VehicleStateId = c.Int(nullable: false),
                        DailyPrice = c.Single(nullable: false),
                        MonthlyPrice = c.Single(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Colours", t => t.ColourId)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.VehicleNumberPassengers", t => t.VehicleSeatId)
                .ForeignKey("dbo.VehicleStates", t => t.VehicleStateId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.ModelId)
                .Index(t => t.CategoryId)
                .Index(t => t.ColourId)
                .Index(t => t.VehicleSeatId)
                .Index(t => t.UserId)
                .Index(t => t.VehicleStateId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Colours",
                c => new
                    {
                        ColourId = c.Int(nullable: false, identity: true),
                        ColourName = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ColourId);
            
            CreateTable(
                "dbo.RentConditions",
                c => new
                    {
                        RentConditionId = c.Int(nullable: false, identity: true),
                        ConditionTitle = c.String(nullable: false, maxLength: 300),
                        ConditionDescription = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RentConditionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        RentId = c.Int(nullable: false, identity: true),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        VehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RentId)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.VehicleNumberPassengers",
                c => new
                    {
                        VehicleCarryId = c.Int(nullable: false),
                        VehicleNumberOfPassengers = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleCarryId);
            
            CreateTable(
                "dbo.VehicleStates",
                c => new
                    {
                        VehicleStateId = c.Int(nullable: false),
                        VehicleStateName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleStateId);
            
            CreateTable(
                "dbo.RentConditionVehicles",
                c => new
                    {
                        RentCondition_RentConditionId = c.Int(nullable: false),
                        Vehicle_VehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RentCondition_RentConditionId, t.Vehicle_VehicleId })
                .ForeignKey("dbo.RentConditions", t => t.RentCondition_RentConditionId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_VehicleId, cascadeDelete: true)
                .Index(t => t.RentCondition_RentConditionId)
                .Index(t => t.Vehicle_VehicleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "VehicleStateId", "dbo.VehicleStates");
            DropForeignKey("dbo.Vehicles", "VehicleSeatId", "dbo.VehicleNumberPassengers");
            DropForeignKey("dbo.Vehicles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rents", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.RentConditionVehicles", "Vehicle_VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.RentConditionVehicles", "RentCondition_RentConditionId", "dbo.RentConditions");
            DropForeignKey("dbo.RentConditions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vehicles", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Vehicles", "ColourId", "dbo.Colours");
            DropForeignKey("dbo.Vehicles", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Vehicles", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropIndex("dbo.RentConditionVehicles", new[] { "Vehicle_VehicleId" });
            DropIndex("dbo.RentConditionVehicles", new[] { "RentCondition_RentConditionId" });
            DropIndex("dbo.Rents", new[] { "VehicleId" });
            DropIndex("dbo.RentConditions", new[] { "UserId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleStateId" });
            DropIndex("dbo.Vehicles", new[] { "UserId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleSeatId" });
            DropIndex("dbo.Vehicles", new[] { "ColourId" });
            DropIndex("dbo.Vehicles", new[] { "CategoryId" });
            DropIndex("dbo.Vehicles", new[] { "ModelId" });
            DropIndex("dbo.Vehicles", new[] { "BrandId" });
            DropIndex("dbo.Models", new[] { "BrandId" });
            DropTable("dbo.RentConditionVehicles");
            DropTable("dbo.VehicleStates");
            DropTable("dbo.VehicleNumberPassengers");
            DropTable("dbo.Rents");
            DropTable("dbo.RentConditions");
            DropTable("dbo.Colours");
            DropTable("dbo.Categories");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Models");
            DropTable("dbo.Brands");
        }
    }
}
