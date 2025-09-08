using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioEstudo.Data.Migrations
{
    public partial class AdicionarDescricao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "tb_Agenda",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "tb_Agenda");
        }
    }
}
