namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Department", "AdminId", "dbo.AspNetUsers");
            DropIndex("dbo.Department", new[] { "AdminId" });
            DropTable("dbo.Department");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        AdminId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Department", "AdminId");
            AddForeignKey("dbo.Department", "AdminId", "dbo.AspNetUsers", "Id");
        }
    }
}
