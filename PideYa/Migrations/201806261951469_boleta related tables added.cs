namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boletarelatedtablesadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.boleta_cabecera",
                c => new
                    {
                        boleta_cabecera_id = c.Int(nullable: false, identity: true),
                        usuarioASP_fk_Id = c.String(maxLength: 128),
                        fecha = c.DateTime(nullable: false, storeType: "date"),
                        cliente = c.String(),
                        estado = c.String(),
                        igv = c.Decimal(nullable: false, storeType: "money"),
                        subtotal = c.Decimal(nullable: false, storeType: "money"),
                        total = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.boleta_cabecera_id)
                .ForeignKey("dbo.AspNetUsers", t => t.usuarioASP_fk_Id)
                .Index(t => t.usuarioASP_fk_Id);
            
            CreateTable(
                "dbo.boleta_detalle",
                c => new
                    {
                        boleta_detalle_id = c.Int(nullable: false, identity: true),
                        boleta_cabecera_id_fk = c.Int(nullable: false),
                        plato_id_fk = c.Int(nullable: false),
                        cantidad = c.Int(nullable: false),
                        total = c.Decimal(nullable: false, storeType: "money"),
                        plato_plato_id = c.Int(),
                    })
                .PrimaryKey(t => t.boleta_detalle_id)
                .ForeignKey("dbo.boleta_cabecera", t => t.boleta_cabecera_id_fk, cascadeDelete: true)
                .ForeignKey("dbo.plato", t => t.plato_plato_id)
                .Index(t => t.boleta_cabecera_id_fk)
                .Index(t => t.plato_plato_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.boleta_cabecera", "usuarioASP_fk_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.boleta_detalle", "plato_plato_id", "dbo.plato");
            DropForeignKey("dbo.boleta_detalle", "boleta_cabecera_id_fk", "dbo.boleta_cabecera");
            DropIndex("dbo.boleta_detalle", new[] { "plato_plato_id" });
            DropIndex("dbo.boleta_detalle", new[] { "boleta_cabecera_id_fk" });
            DropIndex("dbo.boleta_cabecera", new[] { "usuarioASP_fk_Id" });
            DropTable("dbo.boleta_detalle");
            DropTable("dbo.boleta_cabecera");
        }
    }
}
