namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.transportJobs", "transporterID");
            AddForeignKey("dbo.transportJobs", "transporterID", "dbo.user", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.transportJobs", "transporterID", "apollo.user");
            DropIndex("dbo.transportJobs", new[] { "transporterID" });
        }
    }
}
