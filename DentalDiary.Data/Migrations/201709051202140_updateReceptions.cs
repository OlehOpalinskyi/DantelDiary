namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateReceptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "PriceCount", c => c.Double(nullable: false));
            AddColumn("dbo.ReceptionDataModels", "Recivier", c => c.String());
            AddColumn("dbo.ReceptionDataModels", "PriceId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceptionDataModels", "PriceId");
            DropColumn("dbo.ReceptionDataModels", "Recivier");
            DropColumn("dbo.ReceptionDataModels", "PriceCount");
        }
    }
}
