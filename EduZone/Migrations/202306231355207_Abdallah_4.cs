namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Abdallah_4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChatGroups", "IsImage", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChatGroups", "time", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChatGroups", "time");
            DropColumn("dbo.ChatGroups", "IsImage");
        }
    }
}
