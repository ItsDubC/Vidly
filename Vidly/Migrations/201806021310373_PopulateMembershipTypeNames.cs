namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipTypeNames : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET Name = 'Pay as You Go' WHERE DurationInMonths = 0");
            Sql("UPDATE MembershipTypes SET Name = 'Monthly' WHERE DurationInMonths = 1");
            Sql("UPDATE MembershipTypes SET Name = 'Quarterly' WHERE DurationInMonths = 3");
            Sql("UPDATE MembershipTypes SET Name = 'Yearly' WHERE DurationInMonths = 12");
            //Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate, Name) VALUES (1, 0, 0, 0, 'Pay as You Go')");
            //Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate, Name) VALUES (2, 30, 1, 10, 'Monthly')");
            //Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate, Name) VALUES (3, 90, 3, 15, 'Quarterly')");
            //Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate, Name) VALUES (4, 300, 12, 20, 'Yearly')");
        }
        
        public override void Down()
        {
        }
    }
}
