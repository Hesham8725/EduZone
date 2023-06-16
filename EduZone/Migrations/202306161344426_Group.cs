namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Group : DbMigration
    {
        public override void Up()
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
            
            AddColumn("dbo.Educators", "AcademicDegree", c => c.String(nullable: false));
            AlterColumn("dbo.Groups", "CreatorID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Groups", "CreatorID", c => c.Int(nullable: false));
            DropColumn("dbo.Educators", "AcademicDegree");
            DropTable("dbo.GroupsMembers");
        }
    }
}
