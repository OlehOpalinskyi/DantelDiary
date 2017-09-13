namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteREcivier : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReceptionDataModels", "Customer");
            DropColumn("dbo.ReceptionDataModels", "PriceName");
            DropColumn("dbo.ReceptionDataModels", "KindOfWork");
            DropColumn("dbo.ReceptionDataModels", "PriceCount");
            DropColumn("dbo.PersonDataModels", "Recivier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonDataModels", "Recivier", c => c.String());
            AddColumn("dbo.ReceptionDataModels", "PriceCount", c => c.Double(nullable: false));
            AddColumn("dbo.ReceptionDataModels", "KindOfWork", c => c.String());
            AddColumn("dbo.ReceptionDataModels", "PriceName", c => c.String());
            AddColumn("dbo.ReceptionDataModels", "Customer", c => c.String());
        }
    }
}
