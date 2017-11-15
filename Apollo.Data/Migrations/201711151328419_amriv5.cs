namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amriv5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "seen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "seen");
        }
    }
}
