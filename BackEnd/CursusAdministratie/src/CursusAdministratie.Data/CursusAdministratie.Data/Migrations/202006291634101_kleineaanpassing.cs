namespace CursusAdministratie.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kleineaanpassing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursists", "Naam", c => c.String());
            AddColumn("dbo.Cursus", "Duur", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursus", "Duur");
            DropColumn("dbo.Cursists", "Naam");
        }
    }
}
