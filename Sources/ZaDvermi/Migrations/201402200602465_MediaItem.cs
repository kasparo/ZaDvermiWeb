namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MediaItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArticleMedias", "MediaItem_Id", "dbo.MediaItems");
            DropIndex("dbo.ArticleMedias", new[] { "MediaItem_Id" });
            RenameColumn(table: "dbo.ArticleMedias", name: "MediaItem_Id", newName: "MediaItemId");
            AlterColumn("dbo.ArticleMedias", "MediaItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.ArticleMedias", "MediaItemId");
            AddForeignKey("dbo.ArticleMedias", "MediaItemId", "dbo.MediaItems", "Id");
            DropColumn("dbo.ArticleMedias", "MediaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArticleMedias", "MediaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ArticleMedias", "MediaItemId", "dbo.MediaItems");
            DropIndex("dbo.ArticleMedias", new[] { "MediaItemId" });
            AlterColumn("dbo.ArticleMedias", "MediaItemId", c => c.Int());
            RenameColumn(table: "dbo.ArticleMedias", name: "MediaItemId", newName: "MediaItem_Id");
            CreateIndex("dbo.ArticleMedias", "MediaItem_Id");
            AddForeignKey("dbo.ArticleMedias", "MediaItem_Id", "dbo.MediaItems", "Id");
        }
    }
}
