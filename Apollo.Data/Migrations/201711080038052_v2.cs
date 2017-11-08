namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.transportJobs", "title", c => c.String(unicode: false));
            AddColumn("dbo.transportJobs", "description", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.transportJobs", "description");
            DropColumn("dbo.transportJobs", "title");
        }
    }
}
