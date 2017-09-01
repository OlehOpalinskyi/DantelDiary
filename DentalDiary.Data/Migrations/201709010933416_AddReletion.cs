namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReletion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "Price_Id", c => c.Int());
            AddColumn("dbo.PersonDataModels", "Address", c => c.String());
            CreateIndex("dbo.ReceptionDataModels", "Price_Id");
            AddForeignKey("dbo.ReceptionDataModels", "Price_Id", "dbo.PriceDataModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceptionDataModels", "Price_Id", "dbo.PriceDataModels");
            DropIndex("dbo.ReceptionDataModels", new[] { "Price_Id" });
            DropColumn("dbo.PersonDataModels", "Address");
            DropColumn("dbo.ReceptionDataModels", "Price_Id");
        }
    }
}
