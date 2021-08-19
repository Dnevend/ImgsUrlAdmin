namespace FilesUrl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addtypesize2filesurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilesUrls", "Type", c => c.String(unicode: false));
            AddColumn("dbo.FilesUrls", "Size", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilesUrls", "Size");
            DropColumn("dbo.FilesUrls", "Type");
        }
    }
}
