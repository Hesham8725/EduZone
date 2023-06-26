namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departmentandcourses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DoctorOfCourse = c.String(nullable: false),
                        NumberOfHours = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        AdminId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AdminId)
                .Index(t => t.AdminId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Course", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Department", "AdminId", "dbo.AspNetUsers");
            DropIndex("dbo.Department", new[] { "AdminId" });
            DropIndex("dbo.Course", new[] { "DepartmentId" });
            DropTable("dbo.Department");
            DropTable("dbo.Course");
        }
    }
}
