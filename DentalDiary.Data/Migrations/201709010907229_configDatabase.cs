namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceDataModels", "KindOfWork", c => c.String());
            AddColumn("dbo.PersonDataModels", "FullName", c => c.String(maxLength: 70));
            DropColumn("dbo.PersonDataModels", "FirstName");
            DropColumn("dbo.PersonDataModels", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonDataModels", "LastName", c => c.String(maxLength: 50));
            AddColumn("dbo.PersonDataModels", "FirstName", c => c.String(maxLength: 25));
            DropColumn("dbo.PersonDataModels", "FullName");
            DropColumn("dbo.PriceDataModels", "KindOfWork");
        }
    }
}
