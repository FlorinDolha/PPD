using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table("Spectacole")]
    public class Spectacol
    {
        [Key]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        [StringLength(30)]
        public string Titlu { get; set; }

        public double Pret { get; set; }

        public double Sold { get; set; }

        public ICollection<Vanzare> Vanzari { get; set; }
    }
}
