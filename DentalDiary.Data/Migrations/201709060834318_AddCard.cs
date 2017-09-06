namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PesrsonId = c.Int(nullable: false),
                        Complaints = c.String(maxLength: 100),
                        LastTreatment = c.String(maxLength: 100),
                        LastDiagnosis = c.String(maxLength: 100),
                        FinalDiagnosis = c.String(maxLength: 50),
                        AnotherOpinion = c.String(maxLength: 100),
                        TreatmentPlan = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonDataModels", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.PersonDataModels", "CardId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardDataModels", "Id", "dbo.PersonDataModels");
            DropIndex("dbo.CardDataModels", new[] { "Id" });
            DropColumn("dbo.PersonDataModels", "CardId");
            DropTable("dbo.CardDataModels");
        }
    }
}
