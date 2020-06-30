namespace CursusAdministratie.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstantieChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CursusInstanties", "CursistId", "dbo.Cursists");
            DropIndex("dbo.CursusInstanties", new[] { "CursistId" });
            DropPrimaryKey("dbo.CursusInstanties");
            CreateTable(
                "dbo.CursusInstantieCursists",
                c => new
                    {
                        CursusInstantie_Id = c.Int(nullable: false),
                        Cursist_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CursusInstantie_Id, t.Cursist_Id })
                .ForeignKey("dbo.CursusInstanties", t => t.CursusInstantie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cursists", t => t.Cursist_Id, cascadeDelete: true)
                .Index(t => t.CursusInstantie_Id)
                .Index(t => t.Cursist_Id);
            
            AddColumn("dbo.CursusInstanties", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CursusInstanties", "Id");
            DropColumn("dbo.CursusInstanties", "CursistId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CursusInstanties", "CursistId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CursusInstantieCursists", "Cursist_Id", "dbo.Cursists");
            DropForeignKey("dbo.CursusInstantieCursists", "CursusInstantie_Id", "dbo.CursusInstanties");
            DropIndex("dbo.CursusInstantieCursists", new[] { "Cursist_Id" });
            DropIndex("dbo.CursusInstantieCursists", new[] { "CursusInstantie_Id" });
            DropPrimaryKey("dbo.CursusInstanties");
            DropColumn("dbo.CursusInstanties", "Id");
            DropTable("dbo.CursusInstantieCursists");
            AddPrimaryKey("dbo.CursusInstanties", new[] { "CursusId", "CursistId" });
            CreateIndex("dbo.CursusInstanties", "CursistId");
            AddForeignKey("dbo.CursusInstanties", "CursistId", "dbo.Cursists", "Id", cascadeDelete: true);
        }
    }
}
