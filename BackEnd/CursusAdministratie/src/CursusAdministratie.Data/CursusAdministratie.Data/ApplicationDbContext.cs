using CursusAdministratie.Data.Models;
using System.Data.Entity;

namespace CursusAdministratie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cursus> Cursussen { get; set; }
        public DbSet<CursusInstantie> CursusInstanties { get; set; }
        public DbSet<Cursist> Cursisten { get; set; }

        public ApplicationDbContext() : base("CursusAdministratie")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CursusInstantie>()
                 .HasMany(x => x.Cursisten)
                 .WithMany(x => x.Cursussen);
                
            base.OnModelCreating(modelBuilder);
        }
    }

}
