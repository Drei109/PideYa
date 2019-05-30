using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PideYa.Models
{
    [Table("proveedor")]
    public partial class proveedor
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proveedor()
        {
            insumo = new HashSet<insumo>();
        }

        [Key]
        public int proveedor_id { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [StringLength(100)]
        public string telefono { get; set; }

        [StringLength(100)]
        public string correo { get; set; }

        [StringLength(100)]
        public string direccion { get; set; }

        [StringLength(11)]
        public string ruc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insumo> insumo { get; set; }

    }
}