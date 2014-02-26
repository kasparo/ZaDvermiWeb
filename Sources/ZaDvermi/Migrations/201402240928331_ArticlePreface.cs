namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticlePreface : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Preface", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Preface");
        }
    }
}
