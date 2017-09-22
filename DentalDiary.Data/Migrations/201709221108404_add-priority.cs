namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpriority : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceptionDataModels", "Priority");
        }
    }
}
