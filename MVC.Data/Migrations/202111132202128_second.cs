namespace MVC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Book");
            DropColumn("dbo.Book", "ID");
            AddColumn("dbo.Book", "BookId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Book", "BookId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Book");
            DropColumn("dbo.Book", "BookId");
            AddPrimaryKey("dbo.Book", "ID");
        }
    }
}
