using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Avivatec.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IDUSUARIO = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(nullable: true),
                    SOBRENOME = table.Column<string>(nullable: true),
                    EMAIL = table.Column<string>(nullable: true),
                    SENHA = table.Column<string>(nullable: true),
                    LOGIN = table.Column<string>(nullable: true),
                    DATACADASTRO = table.Column<DateTime>(nullable: false),
                    DATAATUALIZACAO = table.Column<DateTime>(nullable: false),
                    ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IDUSUARIO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
