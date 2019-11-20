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
                        Name = c.String(),
                        StreetName = c.String(),
                        City = c.String(),
                        Operator = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        CompanyID = c.String(),
                        PricePerMinute = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        ChargingStationID = c.Int(nullable: false),
                        UserCardID = c.String(maxLength: 128),
                        PaymentMethodID = c.Int(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChargingStations", t => t.ChargingStationID, cascadeDelete: true)
                .ForeignKey("dbo.UserCards", t => t.UserCardID)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodID, cascadeDelete: true)
                .Index(t => t.ChargingStationID)
                .Index(t => t.UserCardID)
                .Index(t => t.PaymentMethodID);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserCardID = c.String(maxLength: 128),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserCards", t => t.UserCardID)
                .Index(t => t.UserCardID);
            
            CreateTable(
                "dbo.UserCards",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "PaymentMethodID", "dbo.PaymentMethods");
            DropForeignKey("dbo.Reservations", "UserCardID", "dbo.UserCards");
            DropForeignKey("dbo.PaymentMethods", "UserCardID", "dbo.UserCards");
            DropForeignKey("dbo.Reservations", "ChargingStationID", "dbo.ChargingStations");
            DropIndex("dbo.PaymentMethods", new[] { "UserCardID" });
            DropIndex("dbo.Reservations", new[] { "PaymentMethodID" });
            DropIndex("dbo.Reservations", new[] { "UserCardID" });
            DropIndex("dbo.Reservations", new[] { "ChargingStationID" });
            DropTable("dbo.UserCards");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.Reservations");
            DropTable("dbo.ChargingStations");
        }
    }
}
