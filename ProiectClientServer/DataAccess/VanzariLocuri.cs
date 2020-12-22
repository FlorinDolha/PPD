using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    [Table("VanzariLocuri")]
    public class VanzariLocuri
    {
        [Key]
        public int Id { get; set; }

        public int Loc { get; set; }

        public int VanzareId { get; set; }

        public Vanzare Vanzare { get; set; }
    }
}
