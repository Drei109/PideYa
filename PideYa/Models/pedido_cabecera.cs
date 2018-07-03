namespace PideYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pedido_cabecera
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pedido_cabecera()
        {
            pedido_detalle = new HashSet<pedido_detalle>();
        }

        [Key]
        public int pedido_cabecera_id { get; set; }

        public int mesa_id_fk { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        [Column(TypeName = "money")]
        public decimal precio_final { get; set; }

        public string estado { get; set; }

        public string token { get; set; }

        public virtual mesa mesa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_detalle> pedido_detalle { get; set; }
    }
}
