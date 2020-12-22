using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrLocuri = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spectacole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Titlu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Pret = table.Column<double>(type: "float", nullable: false),
                    Sold = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spectacole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vanzari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Suma = table.Column<double>(type: "float", nullable: false),
                    NrBileteVandute = table.Column<int>(type: "int", nullable: false),
                    SpectacolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vanzari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vanzari_Spectacole_SpectacolId",
                        column: x => x.SpectacolId,
                        principalTable: "Spectacole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VanzariLocuri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loc = table.Column<int>(type: "int", nullable: false),
                    VanzareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VanzariLocuri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VanzariLocuri_Vanzari_VanzareId",
                        column: x => x.VanzareId,
                        principalTable: "Vanzari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vanzari_SpectacolId",
                table: "Vanzari",
                column: "SpectacolId");

            migrationBuilder.CreateIndex(
                name: "IX_VanzariLocuri_VanzareId",
                table: "VanzariLocuri",
                column: "VanzareId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "VanzariLocuri");

            migrationBuilder.DropTable(
                name: "Vanzari");

            migrationBuilder.DropTable(
                name: "Spectacole");
        }
    }
}
