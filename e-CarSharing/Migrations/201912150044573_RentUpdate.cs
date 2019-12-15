namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RentConditions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RentConditionVehicles", "RentCondition_RentConditionId", "dbo.RentConditions");
            DropForeignKey("dbo.RentConditionVehicles", "Vehicle_VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "VehiclePassengers_NumberPassengersId", "dbo.NumberPassengers");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Brands", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Vehicles", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Rents", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "TypeId", "dbo.Types");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Vehicles", new[] { "VehiclePassengers_NumberPassengersId" });
            DropIndex("dbo.RentConditions", new[] { "UserId" });
            DropIndex("dbo.RentConditionVehicles", new[] { "RentCondition_RentConditionId" });
            DropIndex("dbo.RentConditionVehicles", new[] { "Vehicle_VehicleId" });
            CreateTable(
                "dbo.RentStates",
                c => new
                    {
                        RentStateId = c.Int(nullable: false),
                        RentStateName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RentStateId);
            
            AddColumn("dbo.Rents", "RentStateId", c => c.Int(nullable: false));
            AddColumn("dbo.Rents", "OwnerID", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Rents", "ClientId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Rents", "PickUpLocationId", c => c.Int(nullable: false));
            AddColumn("dbo.Rents", "DeliveryLocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rents", "RentStateId");
            CreateIndex("dbo.Rents", "OwnerID");
            CreateIndex("dbo.Rents", "ClientId");
            CreateIndex("dbo.Rents", "PickUpLocationId");
            CreateIndex("dbo.Rents", "DeliveryLocationId");
            AddForeignKey("dbo.Rents", "ClientId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Rents", "DeliveryLocationId", "dbo.Locations", "LocationId");
            AddForeignKey("dbo.Rents", "OwnerID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Rents", "PickUpLocationId", "dbo.Locations", "LocationId");
            AddForeignKey("dbo.Rents", "RentStateId", "dbo.RentStates", "RentStateId");
            AddForeignKey("dbo.Models", "BrandId", "dbo.Brands", "BrandId");
            AddForeignKey("dbo.Brands", "TypeId", "dbo.Types", "TypeId");
            AddForeignKey("dbo.Vehicles", "LocationId", "dbo.Locations", "LocationId");
            AddForeignKey("dbo.Rents", "VehicleId", "dbo.Vehicles", "VehicleId");
            AddForeignKey("dbo.Vehicles", "TypeId", "dbo.Types", "TypeId");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            DropColumn("dbo.Vehicles", "VehiclePassengers_NumberPassengersId");
            DropTable("dbo.RentConditions");
            DropTable("dbo.NumberPassengers");
            DropTable("dbo.RentConditionVehicles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RentConditionVehicles",
                c => new
                    {
                        RentCondition_RentConditionId = c.Int(nullable: false),
                        Vehicle_VehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RentCondition_RentConditionId, t.Vehicle_VehicleId });
            
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
                "dbo.RentConditions",
                c => new
                    {
                        RentConditionId = c.Int(nullable: false, identity: true),
                        ConditionTitle = c.String(nullable: false, maxLength: 300),
                        ConditionDescription = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RentConditionId);
            
            AddColumn("dbo.Vehicles", "VehiclePassengers_NumberPassengersId", c => c.Int());
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vehicles", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Rents", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Brands", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Rents", "RentStateId", "dbo.RentStates");
            DropForeignKey("dbo.Rents", "PickUpLocationId", "dbo.Locations");
            DropForeignKey("dbo.Rents", "OwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rents", "DeliveryLocationId", "dbo.Locations");
            DropForeignKey("dbo.Rents", "ClientId", "dbo.AspNetUsers");
            DropIndex("dbo.Rents", new[] { "DeliveryLocationId" });
            DropIndex("dbo.Rents", new[] { "PickUpLocationId" });
            DropIndex("dbo.Rents", new[] { "ClientId" });
            DropIndex("dbo.Rents", new[] { "OwnerID" });
            DropIndex("dbo.Rents", new[] { "RentStateId" });
            DropColumn("dbo.Rents", "DeliveryLocationId");
            DropColumn("dbo.Rents", "PickUpLocationId");
            DropColumn("dbo.Rents", "ClientId");
            DropColumn("dbo.Rents", "OwnerID");
            DropColumn("dbo.Rents", "RentStateId");
            DropTable("dbo.RentStates");
            CreateIndex("dbo.RentConditionVehicles", "Vehicle_VehicleId");
            CreateIndex("dbo.RentConditionVehicles", "RentCondition_RentConditionId");
            CreateIndex("dbo.RentConditions", "UserId");
            CreateIndex("dbo.Vehicles", "VehiclePassengers_NumberPassengersId");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Vehicles", "TypeId", "dbo.Types", "TypeId", cascadeDelete: true);
            AddForeignKey("dbo.Rents", "VehicleId", "dbo.Vehicles", "VehicleId", cascadeDelete: true);
            AddForeignKey("dbo.Vehicles", "LocationId", "dbo.Locations", "LocationId", cascadeDelete: true);
            AddForeignKey("dbo.Brands", "TypeId", "dbo.Types", "TypeId", cascadeDelete: true);
            AddForeignKey("dbo.Models", "BrandId", "dbo.Brands", "BrandId", cascadeDelete: true);
            AddForeignKey("dbo.Vehicles", "VehiclePassengers_NumberPassengersId", "dbo.NumberPassengers", "NumberPassengersId");
            AddForeignKey("dbo.RentConditionVehicles", "Vehicle_VehicleId", "dbo.Vehicles", "VehicleId", cascadeDelete: true);
            AddForeignKey("dbo.RentConditionVehicles", "RentCondition_RentConditionId", "dbo.RentConditions", "RentConditionId", cascadeDelete: true);
            AddForeignKey("dbo.RentConditions", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
