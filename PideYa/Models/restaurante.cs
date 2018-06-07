namespace PideYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("restaurante")]
    public partial class restaurante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public restaurante()
        {
            empresa_restaurante_usuario = new HashSet<empresa_restaurante_usuario>();
            mesa = new HashSet<mesa>();
            plato = new HashSet<plato>();
            restaurante_tipo = new HashSet<restaurante_tipo>();
        }

        [Key]
        public int restaurante_id { get; set; }

        public int empresa_id_fk { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [StringLength(100)]
        public string foto { get; set; }

        public virtual empresa empresa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empresa_restaurante_usuario> empresa_restaurante_usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mesa> mesa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<plato> plato { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<restaurante_tipo> restaurante_tipo { get; set; }
    }
}
