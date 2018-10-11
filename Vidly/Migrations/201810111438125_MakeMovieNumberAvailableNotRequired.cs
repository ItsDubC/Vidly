namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeMovieNumberAvailableNotRequired : DbMigration
    {
        public override void Up()
        {
			AlterColumn("dbo.Movies", "NumberAvailable", c => c.Byte(nullable: true));
		}
        
        public override void Down()
        {
			AlterColumn("dbo.Movies", "NumberAvailable", c => c.Int(nullable: false));
		}
    }
}
