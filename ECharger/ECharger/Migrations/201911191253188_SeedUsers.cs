namespace ECharger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'080722bc-6e4d-4e5c-bdef-8909ab534325', N'user@isec.pt', 0, N'ABtLCwSt0TUH++zv+kxYM88BGUSN54IJ+zZOJ9GseqPR8nAdYVAAmo5mlsU4Bu6ltA==', N'be1ebffc-c26f-4452-a8e7-eda24e15d26f', NULL, 0, 0, NULL, 1, 0, N'user@isec.pt')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'420cb643-a40c-46b1-891e-46aa144a2c40', N'company@isec.pt', 0, N'ACQbkVnybVsG3WDOBugYsoGJD97oE7t2Ss1t1VSForo+/MnevbSPzz7n4HJIFQRnJA==', N'8b98e2fb-224a-49f0-a87f-ac90889411a3', NULL, 0, 0, NULL, 1, 0, N'company@isec.pt')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ba971399-84c9-49ee-acdd-96f23053c7db', N'admin@isec.pt', 0, N'ALMS2sQ2o77PtHa9yTLfd4yUoNPfjONtr0fQV62djQNI+7bDV9uiSLJ4JVST2BzEkA==', N'7369f13e-61a1-4fba-ba5c-42cb1bb0f106', NULL, 0, 0, NULL, 1, 0, N'admin@isec.pt')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a045e221-2ffa-414e-b9b3-5a2a87351fde', N'admin')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1228ad91-01df-4d8c-807d-4895986bd374', N'company')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'55218c2d-36cf-4438-9c27-22fbd0a2612e', N'user')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'420cb643-a40c-46b1-891e-46aa144a2c40', N'1228ad91-01df-4d8c-807d-4895986bd374')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'080722bc-6e4d-4e5c-bdef-8909ab534325', N'55218c2d-36cf-4438-9c27-22fbd0a2612e')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ba971399-84c9-49ee-acdd-96f23053c7db', N'a045e221-2ffa-414e-b9b3-5a2a87351fde')


");
        }
        
        public override void Down()
        {
        }
    }
}
