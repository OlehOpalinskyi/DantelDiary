namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class config : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReceptionDataModels", "Price_Id", "dbo.PriceDataModels");
            DropIndex("dbo.ReceptionDataModels", new[] { "Price_Id" });
            DropColumn("dbo.ReceptionDataModels", "Price_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReceptionDataModels", "Price_Id", c => c.Int());
            CreateIndex("dbo.ReceptionDataModels", "Price_Id");
            AddForeignKey("dbo.ReceptionDataModels", "Price_Id", "dbo.PriceDataModels", "Id");
        }
    }
}
