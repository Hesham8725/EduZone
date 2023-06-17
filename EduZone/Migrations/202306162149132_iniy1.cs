namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iniy1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Educators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AcademicDegree = c.String(nullable: false),
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
                        CreatorID = c.String(nullable: false),
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
            
            AddColumn("dbo.Student", "Section", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            DropColumn("dbo.Student", "Age");
            DropColumn("dbo.Student", "Image");
            DropColumn("dbo.Student", "Gender");
            DropColumn("dbo.Student", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Student", "Phone", c => c.String());
            AddColumn("dbo.Student", "Gender", c => c.String());
            AddColumn("dbo.Student", "Image", c => c.String());
            AddColumn("dbo.Student", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.Student", "Section");
            DropTable("dbo.GroupsMembers");
            DropTable("dbo.Groups");
            DropTable("dbo.Educators");
        }
    }
}
