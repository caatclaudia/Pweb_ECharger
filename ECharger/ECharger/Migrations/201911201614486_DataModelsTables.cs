namespace ECharger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataModelsTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChargingStations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        type = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        CompanyID = c.String(nullable: false, maxLength: 128),
                        PricePerMinute = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CompanyID, cascadeDelete: true);


            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        ChargingStationID = c.Int(nullable: false),
                        UserCardID = c.Int(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChargingStations", t => t.ChargingStationID, cascadeDelete: true)
                .ForeignKey("dbo.UserCards", t => t.UserCardID, cascadeDelete: true)
                .Index(t => t.ChargingStationID)
                .Index(t => t.UserCardID);
            
            CreateTable(
                "dbo.UserCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserID = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserCards", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserCardID", "dbo.UserCards");
            DropForeignKey("dbo.PaymentMethods", "UserID", "dbo.UserCards");
            DropForeignKey("dbo.Reservations", "ChargingStationID", "dbo.ChargingStations");
            DropIndex("dbo.PaymentMethods", new[] { "UserID" });
            DropIndex("dbo.Reservations", new[] { "UserCardID" });
            DropIndex("dbo.Reservations", new[] { "ChargingStationID" });
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.UserCards");
            DropTable("dbo.Reservations");
            DropTable("dbo.ChargingStations");
        }
    }
}
