namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amri04v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Content", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Content");
        }
    }
}
