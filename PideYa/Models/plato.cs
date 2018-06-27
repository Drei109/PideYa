namespace PideYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("plato")]
    public partial class plato
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public plato()
        {
            pedido_detalle = new HashSet<pedido_detalle>();
        }

        [Key]
        public int plato_id { get; set; }

        public int restaurante_id_fk { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Column(TypeName = "money")]
        public decimal precio { get; set; }

        public int categoria_plato_id_fk { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string descripcion { get; set; }

        [StringLength(100)]
        public string foto { get; set; }

        [Required]
        [StringLength(100)]
        public string estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_detalle> pedido_detalle { get; set; }

        public virtual plato_categoria plato_categoria { get; set; }

        public virtual restaurante restaurante { get; set; }
    }
}
