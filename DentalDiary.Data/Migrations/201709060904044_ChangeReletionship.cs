namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReletionship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReceptionDataModels", "Preson_Id", "dbo.PersonDataModels");
            DropIndex("dbo.ReceptionDataModels", new[] { "Preson_Id" });
            DropColumn("dbo.ReceptionDataModels", "PersonId");
            RenameColumn(table: "dbo.ReceptionDataModels", name: "Preson_Id", newName: "PersonId");
            AddColumn("dbo.PersonDataModels", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.PersonDataModels", "Debt", c => c.Double(nullable: false));
            AddColumn("dbo.ReceptionDataModels", "KindOfWork", c => c.String());
            AddColumn("dbo.ReceptionDataModels", "Payment", c => c.Double(nullable: false));
            AlterColumn("dbo.ReceptionDataModels", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReceptionDataModels", "PersonId");
            AddForeignKey("dbo.ReceptionDataModels", "PersonId", "dbo.PersonDataModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceptionDataModels", "PersonId", "dbo.PersonDataModels");
            DropIndex("dbo.ReceptionDataModels", new[] { "PersonId" });
            AlterColumn("dbo.ReceptionDataModels", "PersonId", c => c.Int());
            DropColumn("dbo.ReceptionDataModels", "Payment");
            DropColumn("dbo.ReceptionDataModels", "KindOfWork");
            DropColumn("dbo.PersonDataModels", "Debt");
            DropColumn("dbo.PersonDataModels", "DateOfBirth");
            RenameColumn(table: "dbo.ReceptionDataModels", name: "PersonId", newName: "Preson_Id");
            AddColumn("dbo.ReceptionDataModels", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReceptionDataModels", "Preson_Id");
            AddForeignKey("dbo.ReceptionDataModels", "Preson_Id", "dbo.PersonDataModels", "Id");
        }
    }
}
