namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OnlineUSers", "TimeOfOpen", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OnlineUSers", "TimeOfOpen");
        }
    }
}
