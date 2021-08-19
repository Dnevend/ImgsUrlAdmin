namespace FilesUrl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Logs_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.String(unicode: false),
                        subject = c.String(unicode: false),
                        content = c.String(unicode: false),
                        datetime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Log");
        }
    }
}
