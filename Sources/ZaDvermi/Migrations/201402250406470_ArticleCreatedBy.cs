namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleCreatedBy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "CreatedById", "dbo.Users");
            DropIndex("dbo.Articles", new[] { "CreatedById" });
            AlterColumn("dbo.Articles", "CreatedById", c => c.Int());
            CreateIndex("dbo.Articles", "CreatedById");
            AddForeignKey("dbo.Articles", "CreatedById", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "CreatedById", "dbo.Users");
            DropIndex("dbo.Articles", new[] { "CreatedById" });
            AlterColumn("dbo.Articles", "CreatedById", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "CreatedById");
            AddForeignKey("dbo.Articles", "CreatedById", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
