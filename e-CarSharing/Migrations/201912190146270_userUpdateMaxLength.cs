namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userUpdateMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "DriverLicenseNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DriverLicenseNumber", c => c.String(maxLength: 10));
        }
    }
}
