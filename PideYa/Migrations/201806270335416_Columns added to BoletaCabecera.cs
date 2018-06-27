namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnsaddedtoBoletaCabecera : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.boleta_cabecera", "pedido_cabecera_fk_id", c => c.Int());
            CreateIndex("dbo.boleta_cabecera", "pedido_cabecera_fk_id");
            AddForeignKey("dbo.boleta_cabecera", "pedido_cabecera_fk_id", "dbo.pedido_cabecera", "pedido_cabecera_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.boleta_cabecera", "pedido_cabecera_fk_id", "dbo.pedido_cabecera");
            DropIndex("dbo.boleta_cabecera", new[] { "pedido_cabecera_fk_id" });
            DropColumn("dbo.boleta_cabecera", "pedido_cabecera_fk_id");
        }
    }
}
