using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    [Table("Vanzari")]
    [Serializable]
    public class Vanzare
    {
        [Key]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public double Suma { get; set; }

        public int NrBileteVandute { get; set; }

        public ICollection<VanzariLocuri> ListaLocuriVandute { get; set; }

        public int SpectacolId { get; set; }

        public Spectacol Spectacol { get; set; }
    }
}
