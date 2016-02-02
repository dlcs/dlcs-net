namespace iiifly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublicPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PublicPathName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PublicPathName");
        }
    }
}
