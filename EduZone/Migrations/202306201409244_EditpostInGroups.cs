namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditpostInGroups : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.LikeForPostInGroup");
            AddColumn("dbo.LikeForPostInGroup", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.LikeForPostInGroup", "UserID", c => c.String(nullable: false));
            AddPrimaryKey("dbo.LikeForPostInGroup", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.LikeForPostInGroup");
            AlterColumn("dbo.LikeForPostInGroup", "UserID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.LikeForPostInGroup", "Id");
            AddPrimaryKey("dbo.LikeForPostInGroup", "UserID");
        }
    }
}
