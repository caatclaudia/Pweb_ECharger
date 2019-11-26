namespace ECharger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "UserCardID", "dbo.UserCards");
            DropForeignKey("dbo.PaymentMethods", "UserCardID", "dbo.UserCards");
            DropIndex("dbo.Reservations", new[] { "UserCardID" });
            DropIndex("dbo.PaymentMethods", new[] { "UserCardID" });
            AlterColumn("dbo.ChargingStations", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.ChargingStations", "StreetName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.ChargingStations", "City", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.ChargingStations", "CompanyID", c => c.String(nullable: false));
            AlterColumn("dbo.Reservations", "UserCardID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PaymentMethods", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.PaymentMethods", "UserCardID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Reservations", "UserCardID");
            CreateIndex("dbo.PaymentMethods", "UserCardID");
            AddForeignKey("dbo.Reservations", "UserCardID", "dbo.UserCards", "ID");
            AddForeignKey("dbo.PaymentMethods", "UserCardID", "dbo.UserCards", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentMethods", "UserCardID", "dbo.UserCards");
            DropForeignKey("dbo.Reservations", "UserCardID", "dbo.UserCards");
            DropIndex("dbo.PaymentMethods", new[] { "UserCardID" });
            DropIndex("dbo.Reservations", new[] { "UserCardID" });
            AlterColumn("dbo.PaymentMethods", "UserCardID", c => c.String(maxLength: 128));
            AlterColumn("dbo.PaymentMethods", "Name", c => c.String());
            AlterColumn("dbo.Reservations", "UserCardID", c => c.String(maxLength: 128));
            AlterColumn("dbo.ChargingStations", "CompanyID", c => c.String());
            AlterColumn("dbo.ChargingStations", "City", c => c.String());
            AlterColumn("dbo.ChargingStations", "StreetName", c => c.String());
            AlterColumn("dbo.ChargingStations", "Name", c => c.String());
            CreateIndex("dbo.PaymentMethods", "UserCardID");
            CreateIndex("dbo.Reservations", "UserCardID");
            AddForeignKey("dbo.PaymentMethods", "UserCardID", "dbo.UserCards", "ID");
            AddForeignKey("dbo.Reservations", "UserCardID", "dbo.UserCards", "ID");
        }
    }
}
