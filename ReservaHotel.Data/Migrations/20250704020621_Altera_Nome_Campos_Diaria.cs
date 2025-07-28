using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaHotel.Data.Migrations
{
    /// <inheritdoc />
    public partial class Altera_Nome_Campos_Diaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorReal",
                table: "Quartos",
                newName: "DiariaReal");

            migrationBuilder.RenameColumn(
                name: "ValorDolar",
                table: "Quartos",
                newName: "DiariaDolar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiariaReal",
                table: "Quartos",
                newName: "ValorReal");

            migrationBuilder.RenameColumn(
                name: "DiariaDolar",
                table: "Quartos",
                newName: "ValorDolar");
        }
    }
}
