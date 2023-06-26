namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Educator", newName: "Educators");
            DropForeignKey("dbo.Questions", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.AnswerChoices", "QuestionId", "dbo.Questions");
            DropIndex("dbo.AnswerChoices", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "ExamId" });
            DropPrimaryKey("dbo.Educators");
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
            
            AddColumn("dbo.Posts", "ImageUrl", c => c.String());
            AddColumn("dbo.Questions", "CorrectAnswer", c => c.String());
            AddColumn("dbo.Questions", "Point", c => c.Int(nullable: false));
            AddColumn("dbo.Exams", "GroupName", c => c.String(nullable: false));
            AddColumn("dbo.Exams", "CreatorID", c => c.String());
            AddColumn("dbo.Educators", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Student", "Section", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AddPrimaryKey("dbo.Educators", "ID");
            DropColumn("dbo.Exams", "GroupCode");
            DropColumn("dbo.Educators", "NationalID");
            DropColumn("dbo.Educators", "Name");
            DropColumn("dbo.Educators", "Age");
            DropColumn("dbo.Educators", "Image");
            DropColumn("dbo.Educators", "Phone");
            DropColumn("dbo.Educators", "Email");
            DropColumn("dbo.Student", "Age");
            DropColumn("dbo.Student", "Image");
            DropColumn("dbo.Student", "Gender");
            DropColumn("dbo.Student", "Phone");
            DropTable("dbo.AnswerChoices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AnswerChoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Choice = c.String(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Student", "Phone", c => c.String());
            AddColumn("dbo.Student", "Gender", c => c.String());
            AddColumn("dbo.Student", "Image", c => c.String());
            AddColumn("dbo.Student", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Educators", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Educators", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Educators", "Image", c => c.String(nullable: false));
            AddColumn("dbo.Educators", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Educators", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Educators", "NationalID", c => c.String(nullable: false, maxLength: 14));
            AddColumn("dbo.Exams", "GroupCode", c => c.String(nullable: false));
            DropPrimaryKey("dbo.Educators");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.Student", "Section");
            DropColumn("dbo.Educators", "ID");
            DropColumn("dbo.Exams", "CreatorID");
            DropColumn("dbo.Exams", "GroupName");
            DropColumn("dbo.Questions", "Point");
            DropColumn("dbo.Questions", "CorrectAnswer");
            DropColumn("dbo.Posts", "ImageUrl");
            DropTable("dbo.QuestionOptions");
            DropTable("dbo.GroupMaterials");
            AddPrimaryKey("dbo.Educators", "NationalID");
            CreateIndex("dbo.Questions", "ExamId");
            CreateIndex("dbo.AnswerChoices", "QuestionId");
            AddForeignKey("dbo.AnswerChoices", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "ExamId", "dbo.Exams", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Educators", newName: "Educator");
        }
    }
}
