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
                        CompanyID = c.String(maxLength: 128),
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
                        UserCard_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ChargingStations", t => t.ChargingStationID, cascadeDelete: true)
                .ForeignKey("dbo.UserCards", t => t.UserCard_ID)
                .Index(t => t.ChargingStationID)
                .Index(t => t.UserCard_ID);
            
            CreateTable(
                "dbo.UserCards",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ID, cascadeDelete: true);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserID = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        User_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UserCards", t => t.User_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserCard_ID", "dbo.UserCards");
            DropForeignKey("dbo.PaymentMethods", "User_ID", "dbo.UserCards");
            DropForeignKey("dbo.Reservations", "ChargingStationID", "dbo.ChargingStations");
            DropIndex("dbo.PaymentMethods", new[] { "User_ID" });
            DropIndex("dbo.Reservations", new[] { "UserCard_ID" });
            DropIndex("dbo.Reservations", new[] { "ChargingStationID" });
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.UserCards");
            DropTable("dbo.Reservations");
            DropTable("dbo.ChargingStations");
        }
    }
}
