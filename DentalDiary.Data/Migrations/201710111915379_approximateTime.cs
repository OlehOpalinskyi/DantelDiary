namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class approximateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "ApproximateTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceptionDataModels", "ApproximateTime");
        }
    }
}
