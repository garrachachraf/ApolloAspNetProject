namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.toDo", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.toDo", "status");
        }
    }
}
