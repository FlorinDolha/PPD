using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Context : DbContext
    {
        private static Context _context;

        public static Context Instance
        {
            get
            {
                if (_context == null)
                {
                    _context = new Context();
                }

                return _context;
            }
        }

        private Context() { }

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
