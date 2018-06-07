namespace PideYa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class restaurante_tipo
    {
        [Key]
        public int restaurante_tipo_id { get; set; }

        public int restaurante_id_fk { get; set; }

        public int tipo_restaurante_id_fk { get; set; }

        public virtual restaurante restaurante { get; set; }

        public virtual tipo_restaurante tipo_restaurante { get; set; }
    }
}
