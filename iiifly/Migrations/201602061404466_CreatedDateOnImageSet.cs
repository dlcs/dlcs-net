namespace iiifly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedDateOnImageSet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageSets", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageSets", "Created");
        }
    }
}
