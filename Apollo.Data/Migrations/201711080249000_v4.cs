namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.transportJobs", "title", c => c.String(maxLength: 255, storeType: "nvarchar"));
            AlterColumn("dbo.transportJobs", "description", c => c.String(maxLength: 255, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.transportJobs", "description", c => c.String(unicode: false));
            AlterColumn("dbo.transportJobs", "title", c => c.String(unicode: false));
        }
    }
}
