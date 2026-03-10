using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaHotel.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicializando_Postgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CotacoesMoedas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Moeda = table.Column<string>(type: "text", nullable: true),
                    CotacaoCompra = table.Column<decimal>(type: "numeric", nullable: false),
                    CotacaoVenda = table.Column<decimal>(type: "numeric", nullable: true),
                    DataCotacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotacoesMoedas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hoteis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    NumeroQuartos = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Estrelas = table.Column<int>(type: "integer", nullable: false),
                    Andares = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    Senha = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Telefone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false, defaultValue: "100"),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quartos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    DiariaDolar = table.Column<decimal>(type: "numeric", nullable: false),
                    Ocupacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    HotelId = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoQuarto = table.Column<int>(type: "integer", nullable: false),
                    Andar = table.Column<int>(type: "integer", nullable: false),
                    Tamanho = table.Column<float>(type: "real", nullable: false),
                    UltimaAtualizacaoPreco = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DiariaReal = table.Column<decimal>(type: "numeric", nullable: false),
                    CotacaoId = table.Column<Guid>(type: "uuid", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quartos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quartos_CotacoesMoedas_CotacaoId",
                        column: x => x.CotacaoId,
                        principalTable: "CotacoesMoedas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quartos_Hoteis_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hoteis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quartos_CotacaoId",
                table: "Quartos",
                column: "CotacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Quartos_HotelId",
                table: "Quartos",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quartos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "CotacoesMoedas");

            migrationBuilder.DropTable(
                name: "Hoteis");
        }
    }
}
