namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonDataModels", "LinkToImages", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonDataModels", "LinkToImages");
        }
    }
}
