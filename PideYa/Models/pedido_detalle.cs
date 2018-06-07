namespace PideYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pedido_detalle
    {
        [Key]
        public int pedido_detalle_id { get; set; }

        public int pedido_cabecera_id_fk { get; set; }

        public int plato_id_fk { get; set; }

        public int cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal precio { get; set; }

        public virtual pedido_cabecera pedido_cabecera { get; set; }

        public virtual plato plato { get; set; }
    }
}
