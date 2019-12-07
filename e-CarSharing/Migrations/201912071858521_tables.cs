namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
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
                        TypeId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        BrandId = c.Int(),
                        ModelId = c.Int(),
                        VehiclePlate = c.String(),
                        ColourId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        vehiclePassengers = c.Int(nullable: false),
                        VehicleState = c.Int(nullable: false),
                        HourlyPrice = c.Single(nullable: false),
                        DailyPrice = c.Single(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        VehiclePassengers_NumberPassengersId = c.Int(),
                        VehicleState_VehicleStateId = c.Int(),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Colours", t => t.ColourId)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .ForeignKey("dbo.Types", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.NumberPassengers", t => t.VehiclePassengers_NumberPassengersId)
                .ForeignKey("dbo.VehicleStates", t => t.VehicleState_VehicleStateId)
                .Index(t => t.TypeId)
                .Index(t => t.LocationId)
                .Index(t => t.BrandId)
                .Index(t => t.ModelId)
                .Index(t => t.ColourId)
                .Index(t => t.UserId)
                .Index(t => t.VehiclePassengers_NumberPassengersId)
                .Index(t => t.VehicleState_VehicleStateId);
            
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
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        LocationName = c.String(nullable: false, maxLength: 15),
                        GoogleMapsURL = c.String(),
                        Deleted = c.Boolean(nullable: false),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.Location_LocationId);
            
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
                "dbo.Types",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.NumberPassengers",
                c => new
                    {
                        NumberPassengersId = c.Int(nullable: false),
                        NumberOfPassengers = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NumberPassengersId);
            
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
            DropForeignKey("dbo.Vehicles", "VehicleState_VehicleStateId", "dbo.VehicleStates");
            DropForeignKey("dbo.Vehicles", "VehiclePassengers_NumberPassengersId", "dbo.NumberPassengers");
            DropForeignKey("dbo.Vehicles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vehicles", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Rents", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.RentConditionVehicles", "Vehicle_VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.RentConditionVehicles", "RentCondition_RentConditionId", "dbo.RentConditions");
            DropForeignKey("dbo.RentConditions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vehicles", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Vehicles", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Vehicles", "ColourId", "dbo.Colours");
            DropForeignKey("dbo.Vehicles", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropIndex("dbo.RentConditionVehicles", new[] { "Vehicle_VehicleId" });
            DropIndex("dbo.RentConditionVehicles", new[] { "RentCondition_RentConditionId" });
            DropIndex("dbo.Rents", new[] { "VehicleId" });
            DropIndex("dbo.RentConditions", new[] { "UserId" });
            DropIndex("dbo.Locations", new[] { "Location_LocationId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleState_VehicleStateId" });
            DropIndex("dbo.Vehicles", new[] { "VehiclePassengers_NumberPassengersId" });
            DropIndex("dbo.Vehicles", new[] { "UserId" });
            DropIndex("dbo.Vehicles", new[] { "ColourId" });
            DropIndex("dbo.Vehicles", new[] { "ModelId" });
            DropIndex("dbo.Vehicles", new[] { "BrandId" });
            DropIndex("dbo.Vehicles", new[] { "LocationId" });
            DropIndex("dbo.Vehicles", new[] { "TypeId" });
            DropIndex("dbo.Models", new[] { "BrandId" });
            DropTable("dbo.RentConditionVehicles");
            DropTable("dbo.VehicleStates");
            DropTable("dbo.NumberPassengers");
            DropTable("dbo.Types");
            DropTable("dbo.Rents");
            DropTable("dbo.RentConditions");
            DropTable("dbo.Locations");
            DropTable("dbo.Colours");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Models");
            DropTable("dbo.Brands");
        }
    }
}
