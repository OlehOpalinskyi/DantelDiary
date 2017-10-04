namespace DentalDiary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceptionDataModels", "Comment", c => c.String());
            DropColumn("dbo.ReceptionDataModels", "Recomendation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReceptionDataModels", "Recomendation", c => c.String());
            DropColumn("dbo.ReceptionDataModels", "Comment");
        }
    }
}
