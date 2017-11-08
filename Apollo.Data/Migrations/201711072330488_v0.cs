namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.transportJobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DateDeDebut = c.DateTime(nullable: false, precision: 0),
                        DateDeDefin = c.DateTime(nullable: false, precision: 0),
                        orders_Id = c.Int(),
                        tarnsporter_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
               
                .Index(t => t.orders_Id)
                .Index(t => t.tarnsporter_id);
            AddForeignKey("dbo.transportJobs", "orders_Id", "dbo.orders","id");
            AddForeignKey("dbo.transportJobs", "tarnsporter_id", "dbo.user", "id"); 


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.transportJobs", "tarnsporter_id", "apollo.user");
            DropForeignKey("dbo.transportJobs", "orders_Id", "apollo.orders");
            DropIndex("dbo.transportJobs", new[] { "tarnsporter_id" });
            DropIndex("dbo.transportJobs", new[] { "orders_Id" });
            DropTable("dbo.transportJobs");
        }
    }
}
