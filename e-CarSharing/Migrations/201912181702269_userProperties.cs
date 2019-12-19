namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CompanyName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "CC", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "DriverLicenseNumber", c => c.String(maxLength: 10));
            DropColumn("dbo.AspNetUsers", "DriverLicenseEmissionDate");
            DropColumn("dbo.AspNetUsers", "DriverLicenseEndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DriverLicenseEndDate", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DriverLicenseEmissionDate", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "DriverLicenseNumber", c => c.String(maxLength: 20));
            DropColumn("dbo.AspNetUsers", "CC");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "CompanyName");
        }
    }
}
