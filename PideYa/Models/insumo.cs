using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PideYa.Models
{
    [Table("insumo")]
    public partial class insumo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public insumo()
        {
            receta_detalle = new HashSet<receta_detalle>();
        }

        [Key]
        public int insumo_id { get; set; }

        [Required]
        public int proveedor_id_fk { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Column(TypeName = "money")]
        public decimal precio_unitario { get; set; }

        public int cantidad { get; set; }

        [StringLength(100)]
        public string estado { get; set; }

        [ForeignKey("proveedor_id_fk")]
        public virtual proveedor proveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<receta_detalle> receta_detalle { get; set; }
    }
}