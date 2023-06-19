namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Educator");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupsMembers");
        }
        
        public override void Down()
        {
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
            
        }
    }
}
