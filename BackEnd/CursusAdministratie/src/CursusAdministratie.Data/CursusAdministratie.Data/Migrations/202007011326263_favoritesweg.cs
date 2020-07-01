namespace CursusAdministratie.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favoritesweg : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Favorites");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Jaar = c.Int(nullable: false),
                        Week = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
