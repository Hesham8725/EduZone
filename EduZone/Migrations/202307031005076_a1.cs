namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Department", "AdminId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostInGroup", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.LikeForPostInGroup", "PostId", "dbo.PostInGroup");
            DropIndex("dbo.Department", new[] { "AdminId" });
            DropIndex("dbo.LikeForPostInGroup", new[] { "PostId" });
            DropIndex("dbo.PostInGroup", new[] { "GroupId" });
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Level = c.String(nullable: false),
                        Semester = c.String(nullable: false),
                        DoctorOfCourse = c.String(nullable: false),
                        NumberOfHours = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.P_Registration",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Educators", "CVURL", c => c.String());
            AddColumn("dbo.Educators", "Available", c => c.String());
            AddColumn("dbo.Educators", "office", c => c.String());
            AddColumn("dbo.LikeForPostInGroup", "PostInGroup_Id", c => c.Int());
            AlterColumn("dbo.Department", "AdminId", c => c.String());
            AlterColumn("dbo.Educators", "AcademicDegree", c => c.String());
            AlterColumn("dbo.PostInGroup", "GroupId", c => c.String());
            CreateIndex("dbo.LikeForPostInGroup", "PostInGroup_Id");
            AddForeignKey("dbo.LikeForPostInGroup", "PostInGroup_Id", "dbo.PostInGroup", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeForPostInGroup", "PostInGroup_Id", "dbo.PostInGroup");
            DropIndex("dbo.LikeForPostInGroup", new[] { "PostInGroup_Id" });
            AlterColumn("dbo.PostInGroup", "GroupId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Educators", "AcademicDegree", c => c.String(nullable: false));
            AlterColumn("dbo.Department", "AdminId", c => c.String(maxLength: 128));
            DropColumn("dbo.LikeForPostInGroup", "PostInGroup_Id");
            DropColumn("dbo.Educators", "office");
            DropColumn("dbo.Educators", "Available");
            DropColumn("dbo.Educators", "CVURL");
            DropTable("dbo.P_Registration");
            DropTable("dbo.Course");
            CreateIndex("dbo.PostInGroup", "GroupId");
            CreateIndex("dbo.LikeForPostInGroup", "PostId");
            CreateIndex("dbo.Department", "AdminId");
            AddForeignKey("dbo.LikeForPostInGroup", "PostId", "dbo.PostInGroup", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostInGroup", "GroupId", "dbo.Groups", "Code");
            AddForeignKey("dbo.Department", "AdminId", "dbo.AspNetUsers", "Id");
        }
    }
}
