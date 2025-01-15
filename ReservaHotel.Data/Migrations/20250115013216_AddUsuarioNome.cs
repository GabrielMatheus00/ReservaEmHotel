using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaHotel.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioNome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Usuarios");
        }
    }
}
