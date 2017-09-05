namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePerson1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonDataModels", "Recivier", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonDataModels", "Recivier");
        }
    }
}
