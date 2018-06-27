namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class igvcolumndeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.boleta_cabecera", "igv");
        }
        
        public override void Down()
        {
            AddColumn("dbo.boleta_cabecera", "igv", c => c.Decimal(nullable: false, storeType: "money"));
        }
    }
}
