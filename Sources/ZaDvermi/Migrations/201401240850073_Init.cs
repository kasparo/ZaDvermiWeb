namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        Content = c.String(storeType: "ntext"),
                        ParentArticleId = c.Int(),
                        CreatedById = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ArticleType = c.Int(nullable: false),
                        ValidFrom = c.DateTime(),
                        ValidTo = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.ParentArticleId)
                .Index(t => t.CreatedById)
                .Index(t => t.ParentArticleId);
            
            CreateTable(
                "dbo.ArticleMedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        MediaId = c.Int(nullable: false),
                        Title = c.String(maxLength: 60),
                        Description = c.String(),
                        Thumbnail = c.Boolean(nullable: false),
                        MediaItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.MediaItems", t => t.MediaItem_Id)
                .Index(t => t.ArticleId)
                .Index(t => t.MediaItem_Id);
            
            CreateTable(
                "dbo.MediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaType = c.Int(nullable: false),
                        OriginialFileName = c.String(nullable: false),
                        FileName = c.String(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById, cascadeDelete: true)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        NickName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 10),
                        Address = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Password = c.String(nullable: false),
                        LastActivity = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberShips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.UserGroupId);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureId = c.Int(nullable: false),
                        UserGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => t.FeatureId)
                .Index(t => t.UserGroupId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureKey = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "ParentArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ArticleMedias", "MediaItem_Id", "dbo.MediaItems");
            DropForeignKey("dbo.Rights", "UserGroupId", "dbo.UserGroups");
            DropForeignKey("dbo.Rights", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.MemberShips", "UserGroupId", "dbo.UserGroups");
            DropForeignKey("dbo.MemberShips", "UserId", "dbo.Users");
            DropForeignKey("dbo.MediaItems", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ArticleMedias", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Articles", new[] { "ParentArticleId" });
            DropIndex("dbo.Articles", new[] { "CreatedById" });
            DropIndex("dbo.ArticleMedias", new[] { "MediaItem_Id" });
            DropIndex("dbo.Rights", new[] { "UserGroupId" });
            DropIndex("dbo.Rights", new[] { "FeatureId" });
            DropIndex("dbo.MemberShips", new[] { "UserGroupId" });
            DropIndex("dbo.MemberShips", new[] { "UserId" });
            DropIndex("dbo.MediaItems", new[] { "CreatedById" });
            DropIndex("dbo.ArticleMedias", new[] { "ArticleId" });
            DropTable("dbo.Features");
            DropTable("dbo.Rights");
            DropTable("dbo.UserGroups");
            DropTable("dbo.MemberShips");
            DropTable("dbo.Users");
            DropTable("dbo.MediaItems");
            DropTable("dbo.ArticleMedias");
            DropTable("dbo.Articles");
        }
    }
}
