namespace EduZone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
<<<<<<<< HEAD:EduZone/Migrations/202306181811206_Abdallah_1.cs
    public partial class Abdallah_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "image", c => c.String());
========
    public partial class im : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "ImageUrl", c => c.String());
>>>>>>>> master:EduZone/Migrations/202306240856118_im.cs
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ImageUrl");
        }
    }
}
