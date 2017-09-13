namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReletionship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "PriceDataModel_Id", c => c.Int());
            CreateIndex("dbo.ReceptionDataModels", "PriceId");
            CreateIndex("dbo.ReceptionDataModels", "PriceDataModel_Id");
            AddForeignKey("dbo.ReceptionDataModels", "PriceId", "dbo.PriceDataModels", "Id");
            AddForeignKey("dbo.ReceptionDataModels", "PriceDataModel_Id", "dbo.PriceDataModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceptionDataModels", "PriceDataModel_Id", "dbo.PriceDataModels");
            DropForeignKey("dbo.ReceptionDataModels", "PriceId", "dbo.PriceDataModels");
            DropIndex("dbo.ReceptionDataModels", new[] { "PriceDataModel_Id" });
            DropIndex("dbo.ReceptionDataModels", new[] { "PriceId" });
            DropColumn("dbo.ReceptionDataModels", "PriceDataModel_Id");
        }
    }
}
