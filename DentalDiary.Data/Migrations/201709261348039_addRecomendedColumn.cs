namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRecomendedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "Recomended", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceptionDataModels", "Recomended");
        }
    }
}
