namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amriv2 : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Messages", "ConversationID", "dbo.Conversations", "Id");
            AddForeignKey("dbo.Messages", "SenderID", "dbo.user", "id");

            AddForeignKey("dbo.Conversations", "ClientID", "dbo.user", "id");
            AddForeignKey("dbo.Conversations", "AdminID", "dbo.user", "id");
        }
        
        public override void Down()
        {
        }
    }
}
