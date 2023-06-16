namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EducatorProfile1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            DropColumn("dbo.Student", "Age");
            DropColumn("dbo.Student", "Image");
            DropColumn("dbo.Student", "Gender");
            DropColumn("dbo.Student", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Student", "Phone", c => c.String());
            AddColumn("dbo.Student", "Gender", c => c.String());
            AddColumn("dbo.Student", "Image", c => c.String());
            AddColumn("dbo.Student", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "Age");
        }
    }
}
