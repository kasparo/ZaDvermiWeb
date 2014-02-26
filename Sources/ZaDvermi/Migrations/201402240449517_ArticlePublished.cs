namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticlePublished : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Published", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Published");
        }
    }
}
