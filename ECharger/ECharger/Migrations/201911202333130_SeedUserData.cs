namespace ECharger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUserData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[UserCards] ([ID], [Email]) VALUES (N'080722bc-6e4d-4e5c-bdef-8909ab534325', N'user@isec.pt')
");
        }
        
        public override void Down()
        {
        }
    }
}
