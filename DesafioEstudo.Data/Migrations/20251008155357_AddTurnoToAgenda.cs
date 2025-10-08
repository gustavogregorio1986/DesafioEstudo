using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioEstudo.Data.Migrations
{
    public partial class AddTurnoToAgenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Turno",
                table: "tb_Agenda",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Turno",
                table: "tb_Agenda");
        }
    }
}
