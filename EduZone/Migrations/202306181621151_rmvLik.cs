namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmvLik : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "Likes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
        }
    }
}
