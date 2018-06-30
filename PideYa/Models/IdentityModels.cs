using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PideYa.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=RestauranteContext", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<empresa> empresa { get; set; }
        public virtual DbSet<empresa_restaurante_usuario> empresa_restaurante_usuario { get; set; }
        public virtual DbSet<estado_empresa> estado_empresa { get; set; }
        public virtual DbSet<estado_usuario> estado_usuario { get; set; }
        public virtual DbSet<mesa> mesa { get; set; }
        public virtual DbSet<pedido_cabecera> pedido_cabecera { get; set; }
        public virtual DbSet<pedido_detalle> pedido_detalle { get; set; }
        public virtual DbSet<plato> plato { get; set; }
        public virtual DbSet<plato_categoria> plato_categoria { get; set; }
        public virtual DbSet<restaurante> restaurante { get; set; }
        public virtual DbSet<restaurante_tipo> restaurante_tipo { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tipo_restaurante> tipo_restaurante { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
        public virtual DbSet<usuario_tipo> usuario_tipo { get; set; }
        public virtual DbSet<boleta_cabecera> boleta_cabecera { get; set; }
        public virtual DbSet<boleta_detalle> boleta_detalle { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<empresa>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<empresa>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<empresa>()
                .HasMany(e => e.empresa_restaurante_usuario)
                .WithRequired(e => e.empresa)
                .HasForeignKey(e => e.empresa_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<empresa>()
                .HasMany(e => e.restaurante)
                .WithRequired(e => e.empresa)
                .HasForeignKey(e => e.empresa_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<estado_empresa>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<estado_empresa>()
                .HasMany(e => e.empresa)
                .WithRequired(e => e.estado_empresa)
                .HasForeignKey(e => e.estado_empresa_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<estado_usuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<estado_usuario>()
                .HasMany(e => e.usuario)
                .WithRequired(e => e.estado_usuario)
                .HasForeignKey(e => e.estado_usuario_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<mesa>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<mesa>()
                .HasMany(e => e.pedido_cabecera)
                .WithRequired(e => e.mesa)
                .HasForeignKey(e => e.mesa_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pedido_cabecera>()
                .Property(e => e.precio_final)
                .HasPrecision(19, 4);

            modelBuilder.Entity<pedido_cabecera>()
                .HasMany(e => e.pedido_detalle)
                .WithRequired(e => e.pedido_cabecera)
                .HasForeignKey(e => e.pedido_cabecera_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pedido_detalle>()
                .Property(e => e.precio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<plato>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<plato>()
                .Property(e => e.precio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<plato>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<plato>()
                .Property(e => e.foto)
                .IsUnicode(false);

            modelBuilder.Entity<plato>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<plato>()
                .HasMany(e => e.pedido_detalle)
                .WithRequired(e => e.plato)
                .HasForeignKey(e => e.plato_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<plato_categoria>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<plato_categoria>()
                .HasMany(e => e.plato)
                .WithRequired(e => e.plato_categoria)
                .HasForeignKey(e => e.categoria_plato_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<restaurante>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<restaurante>()
                .Property(e => e.foto)
                .IsUnicode(false);

            modelBuilder.Entity<restaurante>()
                .HasMany(e => e.empresa_restaurante_usuario)
                .WithOptional(e => e.restaurante)
                .HasForeignKey(e => e.restaurante_id_fk);

            modelBuilder.Entity<restaurante>()
                .HasMany(e => e.mesa)
                .WithRequired(e => e.restaurante)
                .HasForeignKey(e => e.restaurante_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<restaurante>()
                .HasMany(e => e.plato)
                .WithRequired(e => e.restaurante)
                .HasForeignKey(e => e.restaurante_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<restaurante>()
                .HasMany(e => e.restaurante_tipo)
                .WithRequired(e => e.restaurante)
                .HasForeignKey(e => e.restaurante_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipo_restaurante>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<tipo_restaurante>()
                .HasMany(e => e.restaurante_tipo)
                .WithRequired(e => e.tipo_restaurante)
                .HasForeignKey(e => e.tipo_restaurante_id_fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.usuario1)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.usuario_password)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<usuario>()
                .Property(e => e.dni)
                .IsUnicode(false);

            modelBuilder.Entity<usuario_tipo>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<usuario_tipo>()
                .HasMany(e => e.usuario)
                .WithRequired(e => e.usuario_tipo)
                .HasForeignKey(e => e.usuario_tipo_id_fk)
                .WillCascadeOnDelete(false);
        }
    }
}