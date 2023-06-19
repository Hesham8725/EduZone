namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Abdallah_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "image");
        }
    }
}
