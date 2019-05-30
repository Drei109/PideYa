namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cantidadinrecetachangedtodecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.receta_detalle", "cantidad", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.receta_detalle", "cantidad", c => c.Int(nullable: false));
        }
    }
}
