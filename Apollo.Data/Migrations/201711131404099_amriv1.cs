namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class amriv1 : DbMigration
    {
        public override void Up()
        {
            /*
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(),
                        AdminID = c.Int(),
                        Object = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
           //     .ForeignKey("apollo.user", t => t.AdminID)
            //    .ForeignKey("apollo.user", t => t.ClientID)
                .Index(t => t.ClientID)
                .Index(t => t.AdminID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderID = c.Int(),
                        date = c.DateTime(nullable: false, precision: 0),
                        ConversationID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
           //     .ForeignKey("dbo.Conversations", t => t.ConversationID)
            //    .ForeignKey("apollo.user", t => t.SenderID)
                .Index(t => t.SenderID)
                .Index(t => t.ConversationID);*/
            AddForeignKey("dbo.Message", "ConversationID", "dbo.Conversation", "Id");
            AddForeignKey("dbo.Message", "SenderID", "dbo.user", "id");

            AddForeignKey("dbo.Conversation", "ClientID", "dbo.user", "id");
            AddForeignKey("dbo.Conversation", "AdminID", "dbo.user", "id");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "SenderID", "apollo.user");
            DropForeignKey("dbo.Messages", "ConversationID", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "ClientID", "apollo.user");
            DropForeignKey("dbo.Conversations", "AdminID", "apollo.user");
            DropIndex("dbo.Messages", new[] { "ConversationID" });
            DropIndex("dbo.Messages", new[] { "SenderID" });
            DropIndex("dbo.Conversations", new[] { "AdminID" });
            DropIndex("dbo.Conversations", new[] { "ClientID" });
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
        }
    }
}
