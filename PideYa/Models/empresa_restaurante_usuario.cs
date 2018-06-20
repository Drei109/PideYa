using Microsoft.AspNet.Identity.EntityFramework;

namespace PideYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class empresa_restaurante_usuario
    {
        [Key]
        public int restaurante_usuario_id { get; set; }

        public int empresa_id_fk { get; set; }

        public int? restaurante_id_fk { get; set; }

        [StringLength(128)]
        public string usuarioASP_fk_Id { get; set; }

        public virtual empresa empresa { get; set; }

        public virtual restaurante restaurante { get; set; }

        [ForeignKey("usuarioASP_fk_Id")]
        public virtual ApplicationUser user { get; set; }
    }
}
