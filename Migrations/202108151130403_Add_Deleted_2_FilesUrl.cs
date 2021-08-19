namespace FilesUrl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Deleted_2_FilesUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilesUrls", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FilesUrls", "Deleted");
        }
    }
}
