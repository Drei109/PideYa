using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PideYa.Models
{
    public class boleta_detalle
    {
        [Key]
        public int boleta_detalle_id { get; set; }

        public int boleta_cabecera_id_fk { get; set; }

        public int plato_id_fk { get; set; }

        public int cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal total { get; set; }

        [ForeignKey("boleta_cabecera_id_fk")]
        public virtual boleta_cabecera boleta_cabecera { get; set; }

        public virtual plato plato { get; set; }
    }
}