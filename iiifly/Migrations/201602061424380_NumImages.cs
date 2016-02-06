namespace iiifly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageSets", "NumberOfImages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageSets", "NumberOfImages");
        }
    }
}
