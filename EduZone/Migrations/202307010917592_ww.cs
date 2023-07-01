namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ww : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Department", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeForPostInGroup", "PostId", "dbo.PostInGroup");
            DropIndex("dbo.Department", new[] { "AdminId" });
            DropIndex("dbo.LikeForPostInGroup", new[] { "PostId" });
            AddColumn("dbo.LikeForPostInGroup", "PostInGroup_Id", c => c.Int());
            AlterColumn("dbo.Department", "AdminId", c => c.String());
            CreateIndex("dbo.LikeForPostInGroup", "PostInGroup_Id");
            AddForeignKey("dbo.LikeForPostInGroup", "PostInGroup_Id", "dbo.PostInGroup", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeForPostInGroup", "PostInGroup_Id", "dbo.PostInGroup");
            DropIndex("dbo.LikeForPostInGroup", new[] { "PostInGroup_Id" });
            AlterColumn("dbo.Department", "AdminId", c => c.String(maxLength: 128));
            DropColumn("dbo.LikeForPostInGroup", "PostInGroup_Id");
            CreateIndex("dbo.LikeForPostInGroup", "PostId");
            CreateIndex("dbo.Department", "AdminId");
            AddForeignKey("dbo.LikeForPostInGroup", "PostId", "dbo.PostInGroup", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Department", "AdminId", "dbo.AspNetUsers", "Id");
        }
    }
}
