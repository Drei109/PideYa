namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cantidadininsumochangedtodecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.insumo", "cantidad", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.insumo", "cantidad", c => c.Int(nullable: false));
        }
    }
}
