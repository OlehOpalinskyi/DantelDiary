namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReletionshipPrice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReceptionDataModels", "PersonId", "dbo.PersonDataModels");
            DropForeignKey("dbo.ReceptionDataModels", "CityId", "dbo.CityDataModels");
            DropForeignKey("dbo.PriceDataModels", "CityId", "dbo.CityDataModels");
            AddColumn("dbo.ReceptionDataModels", "PriceDataModel_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ReceptionDataModels", "PriceId");
            CreateIndex("dbo.ReceptionDataModels", "PriceDataModel_Id");
            AddForeignKey("dbo.ReceptionDataModels", "PriceDataModel_Id", "dbo.PriceDataModels", "Id");
            AddForeignKey("dbo.ReceptionDataModels", "PriceId", "dbo.PriceDataModels", "Id");
            AddForeignKey("dbo.ReceptionDataModels", "PersonId", "dbo.PersonDataModels", "Id");
            AddForeignKey("dbo.ReceptionDataModels", "CityId", "dbo.CityDataModels", "Id");
            AddForeignKey("dbo.PriceDataModels", "CityId", "dbo.CityDataModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceDataModels", "CityId", "dbo.CityDataModels");
            DropForeignKey("dbo.ReceptionDataModels", "CityId", "dbo.CityDataModels");
            DropForeignKey("dbo.ReceptionDataModels", "PersonId", "dbo.PersonDataModels");
            DropForeignKey("dbo.ReceptionDataModels", "PriceId", "dbo.PriceDataModels");
            DropForeignKey("dbo.ReceptionDataModels", "PriceDataModel_Id", "dbo.PriceDataModels");
            DropIndex("dbo.ReceptionDataModels", new[] { "PriceDataModel_Id" });
            DropIndex("dbo.ReceptionDataModels", new[] { "PriceId" });
            DropColumn("dbo.ReceptionDataModels", "PriceDataModel_Id");
            AddForeignKey("dbo.PriceDataModels", "CityId", "dbo.CityDataModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReceptionDataModels", "CityId", "dbo.CityDataModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReceptionDataModels", "PersonId", "dbo.PersonDataModels", "Id", cascadeDelete: true);
        }
    }
}
