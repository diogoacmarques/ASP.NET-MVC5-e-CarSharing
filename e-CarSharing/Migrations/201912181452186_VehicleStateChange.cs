namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleStateChange : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Vehicles", name: "VehicleState_VehicleStateId", newName: "VehicleStateId");
            RenameIndex(table: "dbo.Vehicles", name: "IX_VehicleState_VehicleStateId", newName: "IX_VehicleStateId");
            DropColumn("dbo.Vehicles", "VehicleState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "VehicleState", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Vehicles", name: "IX_VehicleStateId", newName: "IX_VehicleState_VehicleStateId");
            RenameColumn(table: "dbo.Vehicles", name: "VehicleStateId", newName: "VehicleState_VehicleStateId");
        }
    }
}
