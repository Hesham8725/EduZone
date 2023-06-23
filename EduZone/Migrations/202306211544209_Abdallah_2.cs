namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Abdallah_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        UserId = c.String(),
                        Image = c.String(),
                        UserName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        MessageContant = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChatGroups");
        }
    }
}
