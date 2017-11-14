namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdsd : DbMigration
    {
        public override void Up()
        {
            
               
            
            CreateTable(
                "dbo.transportJobs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        transporterID = c.Int(),
                        ordersID = c.Int(),
                        DateDeDebut = c.DateTime(nullable: false, precision: 0),
                        DateDeDefin = c.DateTime(nullable: false, precision: 0),
                        title = c.String(maxLength: 255, storeType: "nvarchar"),
                        description = c.String(maxLength: 255, storeType: "nvarchar"),
                        Status = c.String(maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.id)
                
                .Index(t => t.transporterID)
                .Index(t => t.ordersID);


            AddForeignKey("dbo.transportJobs", "transporterID", "dbo.user", "id");
            AddForeignKey("dbo.transportJobs", "ordersID", "dbo.orders", "id");
        

    
         

    }
        
        public override void Down()
        {
            DropForeignKey("dbo.transportJobs", "transporterID", "dbo.user");
            DropForeignKey("dbo.transportJobs", "ordersID", "dbo.orders");
           
        }
    }
}
