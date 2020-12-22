using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Context : DbContext
    {
        public DbSet<Spectacol> Spectacole { get; set; }
        public DbSet<Vanzare> Vanzari { get; set; }
        public DbSet<VanzariLocuri> VanzariLocuri { get; set; }
        public DbSet<Sala> Sala{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SpectacoleDB;Trusted_Connection=True;");
        }
    }
}
