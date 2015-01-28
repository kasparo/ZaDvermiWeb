namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkedArticles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "LinkedArticle1Id", c => c.Int());
            AddColumn("dbo.Articles", "LinkedArticle2Id", c => c.Int());
            CreateIndex("dbo.Articles", "LinkedArticle1Id");
            CreateIndex("dbo.Articles", "LinkedArticle2Id");
            AddForeignKey("dbo.Articles", "LinkedArticle1Id", "dbo.Articles", "Id");
            AddForeignKey("dbo.Articles", "LinkedArticle2Id", "dbo.Articles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "LinkedArticle2Id", "dbo.Articles");
            DropForeignKey("dbo.Articles", "LinkedArticle1Id", "dbo.Articles");
            DropIndex("dbo.Articles", new[] { "LinkedArticle2Id" });
            DropIndex("dbo.Articles", new[] { "LinkedArticle1Id" });
            DropColumn("dbo.Articles", "LinkedArticle2Id");
            DropColumn("dbo.Articles", "LinkedArticle1Id");
        }
    }
}
