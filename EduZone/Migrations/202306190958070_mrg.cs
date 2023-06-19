namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mrg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.Likes", new[] { "PostId" });
            RenameColumn(table: "dbo.Comments", name: "PostID", newName: "Post_Id");
            CreateTable(
                "dbo.Educator",
                c => new
                    {
                        NationalID = c.String(nullable: false, maxLength: 14),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Image = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        AcademicDegree = c.String(nullable: false),
                        AccountID = c.String(),
                    })
                .PrimaryKey(t => t.NationalID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DateOfCreate = c.DateTime(nullable: false),
                        CreatorID = c.String(nullable: false),
                        image = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.GroupsMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.String(),
                        MemberId = c.String(),
                        IsCreate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Comments", "AuthorName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Posts", "AuthorName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Posts", "Likes", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "Post_Id", c => c.Int());
            CreateIndex("dbo.Comments", "Post_Id");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.Posts", "Id");
            DropColumn("dbo.Comments", "UserId");
            DropColumn("dbo.Posts", "UserName");
            DropColumn("dbo.Posts", "UserId");
            DropTable("dbo.Likes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            AddColumn("dbo.Posts", "UserId", c => c.String());
            AddColumn("dbo.Posts", "UserName", c => c.String());
            AddColumn("dbo.Comments", "UserId", c => c.String());
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            AlterColumn("dbo.Comments", "Post_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "Likes");
            DropColumn("dbo.Posts", "AuthorName");
            DropColumn("dbo.Comments", "AuthorName");
            DropTable("dbo.GroupsMembers");
            DropTable("dbo.Groups");
            DropTable("dbo.Educator");
            RenameColumn(table: "dbo.Comments", name: "Post_Id", newName: "PostID");
            CreateIndex("dbo.Likes", "PostId");
            CreateIndex("dbo.Comments", "PostID");
            AddForeignKey("dbo.Comments", "PostID", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Likes", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
