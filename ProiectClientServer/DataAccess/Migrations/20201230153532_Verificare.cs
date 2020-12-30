using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Verificare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VerificareId",
                table: "Vanzari",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VerificareId",
                table: "Spectacole",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Verificari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sold = table.Column<double>(type: "float", nullable: false),
                    VanzareId = table.Column<double>(type: "float", nullable: false),
                    SpectacolId = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verificari", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vanzari_VerificareId",
                table: "Vanzari",
                column: "VerificareId");

            migrationBuilder.CreateIndex(
                name: "IX_Spectacole_VerificareId",
                table: "Spectacole",
                column: "VerificareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spectacole_Verificari_VerificareId",
                table: "Spectacole",
                column: "VerificareId",
                principalTable: "Verificari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vanzari_Verificari_VerificareId",
                table: "Vanzari",
                column: "VerificareId",
                principalTable: "Verificari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spectacole_Verificari_VerificareId",
                table: "Spectacole");

            migrationBuilder.DropForeignKey(
                name: "FK_Vanzari_Verificari_VerificareId",
                table: "Vanzari");

            migrationBuilder.DropTable(
                name: "Verificari");

            migrationBuilder.DropIndex(
                name: "IX_Vanzari_VerificareId",
                table: "Vanzari");

            migrationBuilder.DropIndex(
                name: "IX_Spectacole_VerificareId",
                table: "Spectacole");

            migrationBuilder.DropColumn(
                name: "VerificareId",
                table: "Vanzari");

            migrationBuilder.DropColumn(
                name: "VerificareId",
                table: "Spectacole");
        }
    }
}
