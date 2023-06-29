namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Course", "NumberOfHours", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Course", "NumberOfHours", c => c.Int(nullable: false));
        }
    }
}
