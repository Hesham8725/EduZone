namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sasa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ContentOfComment = c.String(nullable: false, maxLength: 500),
                        Date = c.DateTime(nullable: false),
                        PostID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentOfPost = c.String(nullable: false, maxLength: 500),
                        UserName = c.String(),
                        UserId = c.String(),
                        Date = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.GroupMaterials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Size = c.String(),
                        Type = c.String(),
                        GroupCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LikeForPostInGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostInGroup", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.PostInGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentOfPost = c.String(nullable: false, maxLength: 500),
                        UserName = c.String(),
                        UserId = c.String(),
                        Date = c.DateTime(nullable: false),
                        GroupId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);
            
            DropTable("dbo.Chats");
            DropTable("dbo.IndividuallyChats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IndividuallyChats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userId = c.String(),
                        ConnectionId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SenderId = c.String(),
                        ReciverId = c.String(),
                        content = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.LikeForPostInGroup", "PostId", "dbo.PostInGroup");
            DropForeignKey("dbo.PostInGroup", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropIndex("dbo.PostInGroup", new[] { "GroupId" });
            DropIndex("dbo.LikeForPostInGroup", new[] { "PostId" });
            DropIndex("dbo.Likes", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropTable("dbo.PostInGroup");
            DropTable("dbo.LikeForPostInGroup");
            DropTable("dbo.GroupMaterials");
            DropTable("dbo.Likes");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
