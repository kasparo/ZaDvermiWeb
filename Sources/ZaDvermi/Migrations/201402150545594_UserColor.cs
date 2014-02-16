namespace ZaDvermi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Color");
        }
    }
}
