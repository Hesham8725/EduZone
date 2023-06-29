namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.P_Registration", "CourseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.P_Registration", "CourseId", c => c.String());
        }
    }
}
