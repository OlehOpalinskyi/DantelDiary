namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColunmRecomendate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonDataModels", "RecomendDate", c => c.DateTime());
            AlterColumn("dbo.ReceptionDataModels", "Date", c => c.DateTime());
            AlterColumn("dbo.PersonDataModels", "FirstVisit", c => c.DateTime());
            AlterColumn("dbo.PersonDataModels", "LastVisit", c => c.DateTime());
            AlterColumn("dbo.PersonDataModels", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonDataModels", "DateOfBirth", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PersonDataModels", "LastVisit", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PersonDataModels", "FirstVisit", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ReceptionDataModels", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.PersonDataModels", "RecomendDate");
        }
    }
}
