namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "image", c => c.String(defaultValue: "Gimage.jpg"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "image");
        }
    }
}
