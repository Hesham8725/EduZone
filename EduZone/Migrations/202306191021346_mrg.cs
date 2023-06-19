namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mrg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            RenameColumn(table: "dbo.Comments", name: "Post_Id", newName: "PostID");
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
            AddColumn("dbo.Comments", "UserId", c => c.String());
            AddColumn("dbo.Posts", "UserName", c => c.String());
            AddColumn("dbo.Posts", "UserId", c => c.String());
            AlterColumn("dbo.Comments", "PostID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "PostID");
            AddForeignKey("dbo.Comments", "PostID", "dbo.Posts", "Id", cascadeDelete: true);
            DropColumn("dbo.Comments", "AuthorName");
            DropColumn("dbo.Posts", "AuthorName");
            DropColumn("dbo.Posts", "Likes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "AuthorName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Comments", "AuthorName", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropIndex("dbo.Likes", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            AlterColumn("dbo.Comments", "PostID", c => c.Int());
            DropColumn("dbo.Posts", "UserId");
            DropColumn("dbo.Posts", "UserName");
            DropColumn("dbo.Comments", "UserId");
            DropTable("dbo.Likes");
            RenameColumn(table: "dbo.Comments", name: "PostID", newName: "Post_Id");
            CreateIndex("dbo.Comments", "Post_Id");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.Posts", "Id");
        }
    }
}
