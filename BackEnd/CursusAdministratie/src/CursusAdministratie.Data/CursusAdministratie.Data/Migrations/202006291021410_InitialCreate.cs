namespace CursusAdministratie.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cursists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Achternaam = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CursusInstanties",
                c => new
                    {
                        CursusId = c.Int(nullable: false),
                        CursistId = c.Int(nullable: false),
                        StartDatum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CursusId, t.CursistId })
                .ForeignKey("dbo.Cursists", t => t.CursistId, cascadeDelete: true)
                .ForeignKey("dbo.Cursus", t => t.CursusId, cascadeDelete: true)
                .Index(t => t.CursusId)
                .Index(t => t.CursistId);
            
            CreateTable(
                "dbo.Cursus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CursusInstanties", "CursusId", "dbo.Cursus");
            DropForeignKey("dbo.CursusInstanties", "CursistId", "dbo.Cursists");
            DropIndex("dbo.CursusInstanties", new[] { "CursistId" });
            DropIndex("dbo.CursusInstanties", new[] { "CursusId" });
            DropTable("dbo.Cursus");
            DropTable("dbo.CursusInstanties");
            DropTable("dbo.Cursists");
        }
    }
}
