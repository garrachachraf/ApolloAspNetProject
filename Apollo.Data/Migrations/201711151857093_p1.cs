namespace Apollo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class p1 : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.planification",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        startDate = c.DateTime(nullable: false, precision: 0),
                        finishDate = c.DateTime(nullable: false, precision: 0),
                        description = c.String(maxLength: 255, storeType: "nvarchar"),
                        incomeSource = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        amount = c.Int(nullable: false),
                        period = c.String(maxLength: 255, storeType: "nvarchar"),
                        updateDate = c.DateTime(nullable: false, precision: 0),
                        financerId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.financerId);
                AddForeignKey("dbo.planification", "financerId", "dbo.user", "id");

            CreateTable(
                "dbo.toDo",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    toDoStr = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    deadlineDate = c.DateTime(nullable: false, precision: 0),
                    financerId = c.Int(),
                })  
                .PrimaryKey(t => t.id)
                .Index(t => t.financerId);
                AddForeignKey("dbo.toDo", "financerId", "dbo.user", "id");

            //AddColumn("apollo.orders", "totalAmount", c => c.Single(nullable: false));
            DropColumn("dbo.orders", "amount");
        }

        
        public override void Down()
        {
            DropForeignKey("dbo.toDo", "financerId", "apollo.user");
            DropIndex("dbo.toDo", new[] { "financerId" });
            DropTable("dbo.toDo");
            DropForeignKey("dbo.planification", "financerId", "apollo.user");
            DropIndex("dbo.planification", new[] { "financerId" });
            DropTable("dbo.planification");

        }
    }
}
