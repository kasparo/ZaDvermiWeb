namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
          /*  CreateIndex("dbo.ArticleMedias", "ArticleId");
            CreateIndex("dbo.ArticleMedias", "MediaItemId");
            CreateIndex("dbo.Articles", "ParentArticleId");
            CreateIndex("dbo.Articles", "CreatedById");
            CreateIndex("dbo.Articles", "LinkedArticle1Id");
            CreateIndex("dbo.Articles", "LinkedArticle2Id");
            CreateIndex("dbo.MemberShips", "UserId");
            CreateIndex("dbo.MemberShips", "UserGroupId");
            CreateIndex("dbo.MediaItems", "CreatedById");*/
        }
        
        public override void Down()
        {
           /* DropIndex("dbo.MediaItems", new[] { "CreatedById" });
            DropIndex("dbo.MemberShips", new[] { "UserGroupId" });
            DropIndex("dbo.MemberShips", new[] { "UserId" });
            DropIndex("dbo.Articles", new[] { "LinkedArticle2Id" });
            DropIndex("dbo.Articles", new[] { "LinkedArticle1Id" });
            DropIndex("dbo.Articles", new[] { "CreatedById" });
            DropIndex("dbo.Articles", new[] { "ParentArticleId" });
            DropIndex("dbo.ArticleMedias", new[] { "MediaItemId" });
            DropIndex("dbo.ArticleMedias", new[] { "ArticleId" });*/
        }
    }
}
