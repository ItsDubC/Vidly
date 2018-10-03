namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        { 
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1dbf979c-2ba2-4072-9f3d-fe9faeaf66db', N'guest@vidly.com', 0, N'AKPm+bJJnYHMi7G7uVh3S5Yd4vyZNtnMb/ysSUMSS3e9B8auqHyu8Pv6J1zCAdIIfQ==', N'ce8bd0a6-7a26-489d-b2a1-c307b3614121', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b7d2aea0-361b-42f5-b7de-3b3b92067628', N'admin@vidly.com', 0, N'AAP6yhzcpb/yJ1xup97ntqs4IA1vXBbCkgeCNnvXeyRpE8b65DuTvaV/Cop6iqFsZw==', N'babaec1d-877e-409f-80c4-93428d273b56', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b3c0f7e4-8416-42e3-a43c-a9c1fbb111fb', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b7d2aea0-361b-42f5-b7de-3b3b92067628', N'b3c0f7e4-8416-42e3-a43c-a9c1fbb111fb')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
