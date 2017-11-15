namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amriv6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "isSeen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "isSeen");
        }
    }
}
