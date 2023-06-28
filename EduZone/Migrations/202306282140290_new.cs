namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Educators", newName: "Educator");
            DropPrimaryKey("dbo.Educator");
            CreateTable(
                "dbo.AnswerChoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Choice = c.String(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            AddColumn("dbo.Educator", "NationalID", c => c.String(nullable: false, maxLength: 14));
            AddColumn("dbo.Educator", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Educator", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Educator", "Image", c => c.String(nullable: false));
            AddColumn("dbo.Educator", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Educator", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Exams", "GroupCode", c => c.String(nullable: false));
            AddColumn("dbo.Student", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Student", "Image", c => c.String());
            AddColumn("dbo.Student", "Gender", c => c.String());
            AddColumn("dbo.Student", "Phone", c => c.String());
            AddPrimaryKey("dbo.Educator", "NationalID");
            CreateIndex("dbo.Questions", "ExamId");
            AddForeignKey("dbo.Questions", "ExamId", "dbo.Exams", "Id", cascadeDelete: true);
            DropColumn("dbo.Posts", "ImageUrl");
            DropColumn("dbo.Educator", "ID");
            DropColumn("dbo.Exams", "GroupName");
            DropColumn("dbo.Exams", "CreatorID");
            DropColumn("dbo.Questions", "CorrectAnswer");
            DropColumn("dbo.Questions", "Point");
            DropColumn("dbo.Student", "Section");
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropTable("dbo.GroupMaterials");
            DropTable("dbo.QuestionOptions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuestionOptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OptionContent = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        ExamId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
            AddColumn("dbo.Questions", "Point", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "CorrectAnswer", c => c.String());
            AddColumn("dbo.Exams", "CreatorID", c => c.String());
            AddColumn("dbo.Exams", "GroupName", c => c.String(nullable: false));
            AddColumn("dbo.Educator", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Posts", "ImageUrl", c => c.String());
            DropForeignKey("dbo.AnswerChoices", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "ExamId", "dbo.Exams");
            DropIndex("dbo.Questions", new[] { "ExamId" });
            DropIndex("dbo.AnswerChoices", new[] { "QuestionId" });
            DropPrimaryKey("dbo.Educator");
            DropColumn("dbo.Student", "Phone");
            DropColumn("dbo.Student", "Gender");
            DropColumn("dbo.Student", "Image");
            DropColumn("dbo.Student", "Age");
            DropColumn("dbo.Exams", "GroupCode");
            DropColumn("dbo.Educator", "Email");
            DropColumn("dbo.Educator", "Phone");
            DropColumn("dbo.Educator", "Image");
            DropColumn("dbo.Educator", "Age");
            DropColumn("dbo.Educator", "Name");
            DropColumn("dbo.Educator", "NationalID");
            DropTable("dbo.AnswerChoices");
            AddPrimaryKey("dbo.Educator", "ID");
            RenameTable(name: "dbo.Educator", newName: "Educators");
        }
    }
}
