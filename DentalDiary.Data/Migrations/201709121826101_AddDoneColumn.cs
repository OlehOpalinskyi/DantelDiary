namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoneColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "Done", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceptionDataModels", "Done");
        }
    }
}
