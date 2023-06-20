namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postInGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikeForPostInGroup",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
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
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeForPostInGroup", "PostId", "dbo.PostInGroup");
            DropIndex("dbo.LikeForPostInGroup", new[] { "PostId" });
            DropTable("dbo.PostInGroup");
            DropTable("dbo.LikeForPostInGroup");
        }
    }
}
