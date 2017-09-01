namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 25),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        FirstVisit = c.DateTime(nullable: false),
                        LastVisit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReceptionDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer = c.String(),
                        Date = c.DateTime(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Preson_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonDataModels", t => t.Preson_Id)
                .Index(t => t.Preson_Id);
            
            CreateTable(
                "dbo.PriceDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        CityName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceptionDataModels", "Preson_Id", "dbo.PersonDataModels");
            DropIndex("dbo.ReceptionDataModels", new[] { "Preson_Id" });
            DropTable("dbo.PriceDataModels");
            DropTable("dbo.ReceptionDataModels");
            DropTable("dbo.PersonDataModels");
        }
    }
}
