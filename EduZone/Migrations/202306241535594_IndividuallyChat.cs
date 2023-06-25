namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndividuallyChat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndividuallyChats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userId = c.String(),
                        ConnectionId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IndividuallyChats");
        }
    }
}
