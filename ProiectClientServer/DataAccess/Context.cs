using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class Context : DbContext
    {
        private static Context _context;
        private Random random = new Random();

        private DateTime RandomDay()
        {
            DateTime start = new DateTime(2021, 6, 1);
            int range = (start - DateTime.Today).Days;
            return start.AddDays(random.Next(range));
        }

        public void ClearDatabase()
        {
            ClearSet(Verificari);
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
                    Pret = random.Next(5, 30) + Math.Round(random.NextDouble(), 2),
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

        public Context() { }

        public DbSet<Spectacol> Spectacole { get; set; }
        public DbSet<Vanzare> Vanzari { get; set; }
        public DbSet<VanzariLocuri> VanzariLocuri { get; set; }
        public DbSet<Sala> Sala{ get; set; }
        public DbSet<Verificare> Verificari { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SpectacoleDB;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        private void ClearSet<T>(DbSet<T> set) where T : class
        {
            foreach (var entity in set)
            {
                set.Remove(entity);
            }

            SaveChanges();
        }

        private bool VanzareaEstePentruSpectacol(int vanzareId, int spectacolId)
        {
            Vanzare vanzare = Vanzari.Find(vanzareId);

            if (vanzare == null)
            {
                return false;
            }

            return vanzare.SpectacolId == spectacolId;
        }

        /// <summary>
        /// Cauta primul loc liber pentru spectacolId
        /// </summary>
        /// <param name="spectacolId"></param>
        /// <returns>Returneaza un intreg reprezentand locul liber, sau null daca nu exista</returns>
        public int? PrimulLocLiber(int spectacolId)
        {
            IList<int> locuriSala = Enumerable.Range(1, Sala.Find(1).NrLocuri).ToList();

            IList<int> locuriVandute = VanzariLocuri.Where(vanzareLoc => VanzareaEstePentruSpectacol(vanzareLoc.VanzareId, spectacolId))
                                                    .Select(vanzareLoc => vanzareLoc.Loc).ToList();

            try
            {
                return locuriSala.First(loc => !locuriVandute.Contains(loc));
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }
       
    }
}
