namespace iiifly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageSets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUserId = c.String(),
                        Label = c.String(maxLength: 250),
                        Description = c.String(maxLength: 2000),
                        DlcsBatch = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageSets");
        }
    }
}
