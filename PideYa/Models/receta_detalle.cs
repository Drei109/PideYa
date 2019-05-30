using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PideYa.Models
{
    [Table("receta_detalle")]
    public partial class receta_detalle
    {
        [Key, Column(Order = 0)]
        public int plato_id { get; set; }

        [Key, Column(Order = 1)]
        public int insumo_id { get; set; }

        public decimal cantidad { get; set; }

        [ForeignKey("plato_id")]
        public virtual plato plato { get; set; }

        [ForeignKey("insumo_id")]
        public virtual insumo insumo { get; set; }
    }
}