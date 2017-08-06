namespace Newsletter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsletterUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsletterUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewsletterUsers");
        }
    }
}
