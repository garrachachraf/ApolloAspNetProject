namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                    "dbo.transportJobs",
                    c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        transporterID = c.Int(nullable: false),
                        DateDeDebut = c.DateTime(nullable: false, precision: 0),
                        DateDeDefin = c.DateTime(nullable: false, precision: 0),
                        title = c.String(maxLength: 255, storeType: "nvarchar"),
                        description = c.String(maxLength: 255, storeType: "nvarchar"),
                        Status = c.String(maxLength: 255, storeType: "nvarchar"),
                        orders_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.orders_Id);
            AddForeignKey("dbo.transportJobs", "orders_Id", "dbo.orders","id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.transportJobs", "orders_Id", "apollo.orders");
           
            DropIndex("dbo.transportJobs", new[] { "orders_Id" });
            
        }
    }
}
