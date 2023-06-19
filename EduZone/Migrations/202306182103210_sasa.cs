namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupsMembers", "creationData", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupsMembers", "creationData");
        }
    }
}
