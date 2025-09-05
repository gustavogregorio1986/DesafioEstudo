using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioEstudo.Data.Migrations
{
    public partial class Criartabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Agenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataFim = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Assunto = table.Column<string>(type: "VARCHAR(350)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Agenda", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Agenda");
        }
    }
}
