namespace PideYa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.empresa_restaurante_usuario", "usuarioASP_fk_Id", "dbo.AspNetUsers");
            CreateIndex("dbo.empresa_restaurante_usuario", "usuarioASP_fk_Id");
            AddForeignKey("dbo.empresa_restaurante_usuario", "usuarioASP_fk_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.empresa_restaurante_usuario", "usuarioASP_fk_Id", "dbo.AspNetUsers");
            DropIndex("dbo.empresa_restaurante_usuario", new[] { "usuarioASP_fk_Id" });
        }
    }
}
