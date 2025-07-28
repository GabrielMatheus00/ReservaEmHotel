using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaHotel.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_FK_Cotacao_Quarto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CotacaoId",
                table: "Quartos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quartos_CotacaoId",
                table: "Quartos",
                column: "CotacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quartos_CotacoesMoedas_CotacaoId",
                table: "Quartos",
                column: "CotacaoId",
                principalTable: "CotacoesMoedas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quartos_CotacoesMoedas_CotacaoId",
                table: "Quartos");

            migrationBuilder.DropIndex(
                name: "IX_Quartos_CotacaoId",
                table: "Quartos");

            migrationBuilder.DropColumn(
                name: "CotacaoId",
                table: "Quartos");
        }
    }
}
