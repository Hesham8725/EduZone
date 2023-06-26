namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Course", "DepartmentId", "dbo.Department");
            DropIndex("dbo.Course", new[] { "DepartmentId" });
            AlterColumn("dbo.Course", "DepartmentId", c => c.Int());
            CreateIndex("dbo.Course", "DepartmentId");
            AddForeignKey("dbo.Course", "DepartmentId", "dbo.Department", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Course", "DepartmentId", "dbo.Department");
            DropIndex("dbo.Course", new[] { "DepartmentId" });
            AlterColumn("dbo.Course", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Course", "DepartmentId");
            AddForeignKey("dbo.Course", "DepartmentId", "dbo.Department", "Id", cascadeDelete: true);
        }
    }
}
