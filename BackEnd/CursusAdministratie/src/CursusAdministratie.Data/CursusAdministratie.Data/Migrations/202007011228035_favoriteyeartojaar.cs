namespace CursusAdministratie.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favoriteyeartojaar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Favorites", "Jaar", c => c.Int(nullable: false));
            DropColumn("dbo.Favorites", "Year");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Favorites", "Year", c => c.Int(nullable: false));
            DropColumn("dbo.Favorites", "Jaar");
        }
    }
}
