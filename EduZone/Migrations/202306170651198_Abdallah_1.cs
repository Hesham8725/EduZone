namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Abdallah_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentForPostInGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        Contant = c.String(nullable: false, maxLength: 150),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                        PostInTimeLine_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.PostInGroup", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.PostInTimeLines", t => t.PostInTimeLine_Id)
                .Index(t => t.PostId)
                .Index(t => t.UserID)
                .Index(t => t.PostInTimeLine_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        Name = c.String(),
                        NationalID = c.String(),
                        EmailActive = c.Boolean(nullable: false),
                        Age = c.Int(nullable: false),
                        Image = c.String(),
                        Gender = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PostInGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contant = c.String(nullable: false, maxLength: 500),
                        UserID = c.String(maxLength: 128),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.LikeForPostInGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.PostInGroup", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.CommentForPostInTimeLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        Contant = c.String(nullable: false, maxLength: 150),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.PostInTimeLines", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.PostInTimeLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contant = c.String(nullable: false, maxLength: 500),
                        UserID = c.String(maxLength: 128),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.LikeForPostInTimeLine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.PostInTimeLines", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Educators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AcademicDegree = c.String(nullable: false),
                        AccountID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DateOfCreate = c.DateTime(nullable: false),
                        CreatorID = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
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
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CollegeID = c.Int(nullable: false),
                        GroupNo = c.Int(nullable: false),
                        GPA = c.Double(nullable: false),
                        Batch = c.Int(nullable: false),
                        Department = c.String(),
                        Section = c.Int(nullable: false),
                        AccountID = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MailOfDoctors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DoctorMail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CommentForPostInTimeLines", "PostId", "dbo.PostInTimeLines");
            DropForeignKey("dbo.LikeForPostInTimeLine", "PostId", "dbo.PostInTimeLines");
            DropForeignKey("dbo.LikeForPostInTimeLine", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentForPostInGroup", "PostInTimeLine_Id", "dbo.PostInTimeLines");
            DropForeignKey("dbo.PostInTimeLines", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentForPostInTimeLines", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentForPostInGroup", "PostId", "dbo.PostInGroup");
            DropForeignKey("dbo.LikeForPostInGroup", "PostId", "dbo.PostInGroup");
            DropForeignKey("dbo.LikeForPostInGroup", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostInGroup", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentForPostInGroup", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LikeForPostInTimeLine", new[] { "UserID" });
            DropIndex("dbo.LikeForPostInTimeLine", new[] { "PostId" });
            DropIndex("dbo.PostInTimeLines", new[] { "UserID" });
            DropIndex("dbo.CommentForPostInTimeLines", new[] { "UserID" });
            DropIndex("dbo.CommentForPostInTimeLines", new[] { "PostId" });
            DropIndex("dbo.LikeForPostInGroup", new[] { "UserID" });
            DropIndex("dbo.LikeForPostInGroup", new[] { "PostId" });
            DropIndex("dbo.PostInGroup", new[] { "UserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CommentForPostInGroup", new[] { "PostInTimeLine_Id" });
            DropIndex("dbo.CommentForPostInGroup", new[] { "UserID" });
            DropIndex("dbo.CommentForPostInGroup", new[] { "PostId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MailOfDoctors");
            DropTable("dbo.Student");
            DropTable("dbo.GroupsMembers");
            DropTable("dbo.Groups");
            DropTable("dbo.Educators");
            DropTable("dbo.LikeForPostInTimeLine");
            DropTable("dbo.PostInTimeLines");
            DropTable("dbo.CommentForPostInTimeLines");
            DropTable("dbo.LikeForPostInGroup");
            DropTable("dbo.PostInGroup");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CommentForPostInGroup");
        }
    }
}
