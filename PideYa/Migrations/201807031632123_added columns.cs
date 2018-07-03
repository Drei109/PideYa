namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcolumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.pedido_cabecera", "token", c => c.String());
            AddColumn("dbo.plato_categoria", "imagen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.plato_categoria", "imagen");
            DropColumn("dbo.pedido_cabecera", "token");
        }
    }
}
