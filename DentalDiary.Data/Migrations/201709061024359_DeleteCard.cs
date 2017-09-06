namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCard : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CardDataModels", "Id", "dbo.PersonDataModels");
            DropIndex("dbo.CardDataModels", new[] { "Id" });
            AddColumn("dbo.PersonDataModels", "Complaints", c => c.String(maxLength: 100));
            AddColumn("dbo.PersonDataModels", "LastTreatment", c => c.String(maxLength: 100));
            AddColumn("dbo.PersonDataModels", "LastDiagnosis", c => c.String(maxLength: 100));
            AddColumn("dbo.PersonDataModels", "FinalDiagnosis", c => c.String(maxLength: 50));
            AddColumn("dbo.PersonDataModels", "AnotherOpinion", c => c.String(maxLength: 100));
            AddColumn("dbo.PersonDataModels", "TreatmentPlan", c => c.String(maxLength: 150));
            DropColumn("dbo.PersonDataModels", "CardId");
            DropTable("dbo.CardDataModels");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PersonDataModels", "CardId", c => c.Int(nullable: false));
            DropColumn("dbo.PersonDataModels", "TreatmentPlan");
            DropColumn("dbo.PersonDataModels", "AnotherOpinion");
            DropColumn("dbo.PersonDataModels", "FinalDiagnosis");
            DropColumn("dbo.PersonDataModels", "LastDiagnosis");
            DropColumn("dbo.PersonDataModels", "LastTreatment");
            DropColumn("dbo.PersonDataModels", "Complaints");
            CreateIndex("dbo.CardDataModels", "Id");
            AddForeignKey("dbo.CardDataModels", "Id", "dbo.PersonDataModels", "Id");
        }
    }
}
