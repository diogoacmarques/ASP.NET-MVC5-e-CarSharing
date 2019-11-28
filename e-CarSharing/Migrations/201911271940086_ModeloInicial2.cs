namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModeloInicial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brands", "Deleted");
        }
    }
}
