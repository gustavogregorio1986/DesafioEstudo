using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioEstudo.Data.Migrations
{
    public partial class AdicionarColunaEnumSituacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assunto",
                table: "tb_Agenda");

            migrationBuilder.AddColumn<string>(
                name: "Situacao",
                table: "tb_Agenda",
                type: "VARCHAR(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "tb_Agenda");

            migrationBuilder.AddColumn<string>(
                name: "Assunto",
                table: "tb_Agenda",
                type: "VARCHAR(350)",
                nullable: false,
                defaultValue: "");
        }
    }
}
