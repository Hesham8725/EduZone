namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Student", "Image", c => c.String());
            AddColumn("dbo.Student", "Gender", c => c.String());
            AddColumn("dbo.Student", "Phone", c => c.String());
            DropColumn("dbo.Posts", "AuthorName");
            DropColumn("dbo.Posts", "ImageUrl");
            DropColumn("dbo.Student", "Section");
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropTable("dbo.GroupMaterials");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupMaterials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Size = c.String(),
                        Type = c.String(),
                        GroupCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Student", "Section", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "ImageUrl", c => c.String());
            AddColumn("dbo.Posts", "AuthorName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Student", "Phone");
            DropColumn("dbo.Student", "Gender");
            DropColumn("dbo.Student", "Image");
            DropColumn("dbo.Student", "Age");
        }
    }
}
