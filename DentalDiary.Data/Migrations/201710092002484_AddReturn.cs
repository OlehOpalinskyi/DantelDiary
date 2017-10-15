namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReturn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "Return", c => c.Double(nullable: false));
            AddColumn("dbo.ReceptionDataModels", "IsReturn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceptionDataModels", "IsReturn");
            DropColumn("dbo.ReceptionDataModels", "Return");
        }
    }
}
