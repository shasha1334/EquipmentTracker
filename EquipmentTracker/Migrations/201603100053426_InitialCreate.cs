namespace EquipmentTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asset",
                c => new
                    {
                        AssetID = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 50),
                        SerialNumber = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.AssetID);
            
            CreateTable(
                "dbo.WorkOrder",
                c => new
                    {
                        WorkOrderID = c.Int(nullable: false, identity: true),
                        AssetID = c.String(maxLength: 128),
                        Problem = c.String(nullable: false, maxLength: 150),
                        DateRequested = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WorkOrderID)
                .ForeignKey("dbo.Asset", t => t.AssetID)
                .Index(t => t.AssetID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrder", "AssetID", "dbo.Asset");
            DropIndex("dbo.WorkOrder", new[] { "AssetID" });
            DropTable("dbo.WorkOrder");
            DropTable("dbo.Asset");
        }
    }
}
