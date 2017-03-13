namespace InstPublish.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentstext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Text", c => c.String());
            DropColumn("dbo.Comments", "Contetnt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Contetnt", c => c.String());
            DropColumn("dbo.Comments", "Text");
        }
    }
}
