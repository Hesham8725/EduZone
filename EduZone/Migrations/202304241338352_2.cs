namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailOfDoctors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DoctorMail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MailOfDoctors");
        }
    }
}
