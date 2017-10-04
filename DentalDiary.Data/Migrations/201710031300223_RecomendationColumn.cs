namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecomendationColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "Recomendation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceptionDataModels", "Recomendation");
        }
    }
}
