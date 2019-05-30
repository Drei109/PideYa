namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insumopreciounidadcantidadcolumnsaddedormodified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.insumo", "precio", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.insumo", "unidad", c => c.String());
            DropColumn("dbo.insumo", "precio_unitario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.insumo", "precio_unitario", c => c.Decimal(nullable: false, storeType: "money"));
            DropColumn("dbo.insumo", "unidad");
            DropColumn("dbo.insumo", "precio");
        }
    }
}
