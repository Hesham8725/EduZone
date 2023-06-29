namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Course", "Level", c => c.String(nullable: false));
            AddColumn("dbo.Course", "Semester", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Course", "Semester");
            DropColumn("dbo.Course", "Level");
        }
    }
}
