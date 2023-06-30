namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        IsImage = c.Boolean(nullable: false),
                        UserId = c.String(),
                        Image = c.String(),
                        UserName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        time = c.String(),
                        MessageContant = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatIndividuals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsImage = c.Boolean(nullable: false),
                        SenderId = c.String(),
                        ReceiverId = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Time = c.String(),
                        MessageContant = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                        FormTitle = c.String(nullable: false),
                        CreatorID = c.String(),
                        IsStart = c.Boolean(nullable: false),
                        GroupCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CurrantUserIsOnlines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LastMessageInChatIndividuals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SendId = c.String(),
                        ReseverID = c.String(),
                        LastMessage = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OnlineUSers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ConnectionID = c.String(),
                        ReseverId = c.String(),
                        TimeOfOpen = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(nullable: false),
                        CorrectAnswer = c.String(),
                        Point = c.Int(nullable: false),
                        ExamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentAnswers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(nullable: false),
                        QuestionID = c.Int(nullable: false),
                        Answer = c.String(),
                        StudentID = c.String(),
                        AnswerVale = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StudentExamDegrees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(nullable: false),
                        GroupCode = c.String(),
                        StudentID = c.String(),
                        Degree = c.Int(nullable: false),
                        StudentName = c.String(),
                        Sitting_Number = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.GroupsMembers", "TimeGoin", c => c.DateTime(nullable: false));
            AddColumn("dbo.PostInGroup", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostInGroup", "ImageUrl");
            DropColumn("dbo.GroupsMembers", "TimeGoin");
            DropTable("dbo.StudentExamDegrees");
            DropTable("dbo.StudentAnswers");
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionOptions");
            DropTable("dbo.OnlineUSers");
            DropTable("dbo.LastMessageInChatIndividuals");
            DropTable("dbo.CurrantUserIsOnlines");
            DropTable("dbo.Exams");
            DropTable("dbo.ChatIndividuals");
            DropTable("dbo.ChatGroups");
        }
    }
}
