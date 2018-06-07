namespace PideYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("usuario")]
    public partial class usuario
    {
        [Key]
        public int usuario_id { get; set; }

        public int usuario_tipo_id_fk { get; set; }

        public int estado_usuario_id_fk { get; set; }

        [Column("usuario")]
        [Required]
        [StringLength(100)]
        public string usuario1 { get; set; }

        [Required]
        [StringLength(100)]
        public string usuario_password { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(8)]
        public string dni { get; set; }

        public virtual estado_usuario estado_usuario { get; set; }

        public virtual usuario_tipo usuario_tipo { get; set; }
    }
}
