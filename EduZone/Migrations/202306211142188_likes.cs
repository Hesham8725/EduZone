namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likes : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Likes");
            AddColumn("dbo.Likes", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Likes", "UserID", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Likes", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Likes");
            AlterColumn("dbo.Likes", "UserID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Likes", "Id");
            AddPrimaryKey("dbo.Likes", "UserID");
        }
    }
}
