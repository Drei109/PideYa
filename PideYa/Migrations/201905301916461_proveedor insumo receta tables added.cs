namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class proveedorinsumorecetatablesadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.receta_detalle",
                c => new
                    {
                        plato_id = c.Int(nullable: false),
                        insumo_id = c.Int(nullable: false),
                        cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.plato_id, t.insumo_id })
                .ForeignKey("dbo.insumo", t => t.insumo_id)
                .ForeignKey("dbo.plato", t => t.plato_id)
                .Index(t => t.plato_id)
                .Index(t => t.insumo_id);
            
            CreateTable(
                "dbo.insumo",
                c => new
                    {
                        insumo_id = c.Int(nullable: false, identity: true),
                        proveedor_id_fk = c.Int(nullable: false),
                        nombre = c.String(nullable: false, maxLength: 100),
                        precio_unitario = c.Decimal(nullable: false, storeType: "money"),
                        cantidad = c.Int(nullable: false),
                        estado = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.insumo_id)
                .ForeignKey("dbo.proveedor", t => t.proveedor_id_fk)
                .Index(t => t.proveedor_id_fk);
            
            CreateTable(
                "dbo.proveedor",
                c => new
                    {
                        proveedor_id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 100),
                        telefono = c.String(maxLength: 100),
                        correo = c.String(maxLength: 100),
                        direccion = c.String(maxLength: 100),
                        ruc = c.String(maxLength: 11),
                    })
                .PrimaryKey(t => t.proveedor_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.receta_detalle", "plato_id", "dbo.plato");
            DropForeignKey("dbo.receta_detalle", "insumo_id", "dbo.insumo");
            DropForeignKey("dbo.insumo", "proveedor_id_fk", "dbo.proveedor");
            DropIndex("dbo.insumo", new[] { "proveedor_id_fk" });
            DropIndex("dbo.receta_detalle", new[] { "insumo_id" });
            DropIndex("dbo.receta_detalle", new[] { "plato_id" });
            DropTable("dbo.proveedor");
            DropTable("dbo.insumo");
            DropTable("dbo.receta_detalle");
        }
    }
}
