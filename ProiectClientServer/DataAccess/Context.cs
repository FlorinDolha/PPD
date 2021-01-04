using Microsoft.EntityFrameworkCore;
<<<<<<< Updated upstream
using System;
=======
>>>>>>> Stashed changes

namespace DataAccess
{
    public class Context : DbContext
    {
        private static Context _context;
        private Random random = new Random();

        private DateTime RandomDay()
        {
            DateTime start = new DateTime(2020, 6, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        public void ClearDatabase()
        {
            ClearSet(VanzariLocuri);
            ClearSet(Vanzari);
            ClearSet(Spectacole);
        }

        public void GenerateData()
        {
            for (int i = 0; i < 5; i++)
            {
                Spectacol spectacol = new Spectacol
                {
                    Data = RandomDay(),
                    Pret = random.Next(5, 30) + random.NextDouble(),
                    Titlu = $"Spectacol{i}",
                    Sold = 0,
                };

                Spectacole.Add(spectacol);
            }

            SaveChanges();
        }

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

        private void ClearSet<T>(DbSet<T> set) where T : class
        {
            foreach (var entity in set)
            {
                set.Remove(entity);
            }

            SaveChanges();
        }
       
    }
}
