namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        KindOfWork = c.String(),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CityDataModels", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.ReceptionDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer = c.String(),
                        Date = c.DateTime(nullable: false),
                        PriceName = c.String(),
                        KindOfWork = c.String(),
                        PriceCount = c.Double(nullable: false),
                        Recivier = c.String(),
                        Payment = c.Double(nullable: false),
                        PersonId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        PriceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CityDataModels", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.PersonDataModels", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.PersonDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 70),
                        Address = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        FirstVisit = c.DateTime(nullable: false),
                        LastVisit = c.DateTime(nullable: false),
                        LinkToImages = c.String(),
                        Recivier = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Debt = c.Double(nullable: false),
                        Complaints = c.String(maxLength: 100),
                        LastTreatment = c.String(maxLength: 100),
                        LastDiagnosis = c.String(maxLength: 100),
                        FinalDiagnosis = c.String(maxLength: 50),
                        AnotherOpinion = c.String(maxLength: 100),
                        TreatmentPlan = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceptionDataModels", "PersonId", "dbo.PersonDataModels");
            DropForeignKey("dbo.ReceptionDataModels", "CityId", "dbo.CityDataModels");
            DropForeignKey("dbo.PriceDataModels", "CityId", "dbo.CityDataModels");
            DropIndex("dbo.ReceptionDataModels", new[] { "CityId" });
            DropIndex("dbo.ReceptionDataModels", new[] { "PersonId" });
            DropIndex("dbo.PriceDataModels", new[] { "CityId" });
            DropTable("dbo.PersonDataModels");
            DropTable("dbo.ReceptionDataModels");
            DropTable("dbo.PriceDataModels");
            DropTable("dbo.CityDataModels");
        }
    }
}
