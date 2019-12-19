namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentStateUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rents", "RentStateId", "dbo.RentStates");
            DropPrimaryKey("dbo.RentStates");
            AlterColumn("dbo.RentStates", "RentStateId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.RentStates", "RentStateId");
            AddForeignKey("dbo.Rents", "RentStateId", "dbo.RentStates", "RentStateId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rents", "RentStateId", "dbo.RentStates");
            DropPrimaryKey("dbo.RentStates");
            AlterColumn("dbo.RentStates", "RentStateId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.RentStates", "RentStateId");
            AddForeignKey("dbo.Rents", "RentStateId", "dbo.RentStates", "RentStateId");
        }
    }
}
