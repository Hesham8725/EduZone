namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Age = c.Int(nullable: false),
                        Image = c.String(),
                        Gender = c.String(),
                        Phone = c.String(),
                        CollegeID = c.Int(nullable: false),
                        GroupNo = c.Int(nullable: false),
                        GPA = c.Double(nullable: false),
                        Batch = c.Int(nullable: false),
                        AccountID = c.String(),
                        Department = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.Student");
        }
    }
}
