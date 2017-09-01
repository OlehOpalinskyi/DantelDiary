namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "PriceName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceptionDataModels", "PriceName");
        }
    }
}
