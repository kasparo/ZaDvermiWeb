namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRights : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rights", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.Rights", "UserGroupId", "dbo.UserGroups");
            DropIndex("dbo.Rights", new[] { "FeatureId" });
            DropIndex("dbo.Rights", new[] { "UserGroupId" });
            DropTable("dbo.Rights");
            DropTable("dbo.Features");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureKey = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Rights", "UserGroupId");
            CreateIndex("dbo.Rights", "FeatureId");
            AddForeignKey("dbo.Rights", "UserGroupId", "dbo.UserGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rights", "FeatureId", "dbo.Features", "Id", cascadeDelete: true);
        }
    }
}
