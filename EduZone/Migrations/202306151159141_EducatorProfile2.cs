namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EducatorProfile2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Educators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DateOfCreate = c.DateTime(nullable: false),
                        CreatorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            AddColumn("dbo.Student", "Section", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student", "Section");
            DropTable("dbo.Groups");
            DropTable("dbo.Educators");
        }
    }
}
