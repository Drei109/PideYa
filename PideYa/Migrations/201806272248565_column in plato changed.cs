namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columninplatochanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.plato", "foto", c => c.String(maxLength: 100, unicode: false));
        }

        public override void Down() => AlterColumn("dbo.plato", "foto", c => c.String(nullable: false, maxLength: 100, unicode: false));
    }
}
