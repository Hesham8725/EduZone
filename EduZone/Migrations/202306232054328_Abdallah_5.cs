namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Abdallah_5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupsMembers", "TimeGoin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupsMembers", "TimeGoin");
        }
    }
}
