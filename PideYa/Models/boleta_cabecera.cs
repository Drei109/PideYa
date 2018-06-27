using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PideYa.Models
{
    public class boleta_cabecera
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public boleta_cabecera()
        {
            boleta_detalle = new HashSet<boleta_detalle>();
        }

        [Key]
        public int boleta_cabecera_id { get; set; }

        [StringLength(128)]
        public string usuarioASP_fk_Id { get; set; }

        public int? pedido_cabecera_fk_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public string cliente { get; set; }

        public string estado { get; set; }

        [Column(TypeName = "money")]
        public decimal subtotal { get; set; }

        [Column(TypeName = "money")]
        public decimal total { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<boleta_detalle> boleta_detalle { get; set; }

        [ForeignKey("usuarioASP_fk_Id")]
        public virtual ApplicationUser user { get; set; }

        [ForeignKey("pedido_cabecera_fk_id")]
        public virtual pedido_cabecera pedido_cabecera { get; set; }
    }
}