using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess
{
    [Table("Verificari")]
    public class Verificare
    {
        [Key]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public double Sold { get; set; }

        public int SpectacolId { get; set; }

        public Spectacol Spectacol { get; set; }

        public string Status { get; set; }
    }
}
