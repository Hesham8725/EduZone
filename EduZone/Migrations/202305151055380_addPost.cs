namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 500),
                        AuthorName = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false, maxLength: 500),
                        Date = c.DateTime(nullable: false),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
        }
    }
}
