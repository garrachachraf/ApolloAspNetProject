namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.transportJobs", "transporterID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.transportJobs", "transporterID", c => c.Int(nullable: false));
        }
    }
}
