namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Age = c.Int(nullable: false),
                        Image = c.String(),
                        Gender = c.String(),
                        Phone = c.String(),
                        CollegeID = c.Int(nullable: false),
                        GroupNo = c.Int(nullable: false),
                        GPA = c.Double(nullable: false),
                        Batch = c.Int(nullable: false),
                        AccountID = c.String(),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
            
            DropTable("dbo.Student");
            CreateIndex("dbo.Comments", "Post_Id");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.Posts", "Id");
        }
    }
}
