namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditpostInGroup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostInGroup", "GroupId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PostInGroup", "GroupId");
            AddForeignKey("dbo.PostInGroup", "GroupId", "dbo.Groups", "Code");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostInGroup", "GroupId", "dbo.Groups");
            DropIndex("dbo.PostInGroup", new[] { "GroupId" });
            AlterColumn("dbo.PostInGroup", "GroupId", c => c.Int(nullable: false));
        }
    }
}
