namespace PideYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mesa")]
    public partial class mesa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mesa()
        {
            pedido_cabecera = new HashSet<pedido_cabecera>();
        }

        [Key]
        public int mesa_id { get; set; }

        public int restaurante_id_fk { get; set; }

        [Required]
        [StringLength(100)]
        public string estado { get; set; }

        public virtual restaurante restaurante { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pedido_cabecera> pedido_cabecera { get; set; }
    }
}
