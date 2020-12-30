using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class VerificareV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spectacole_Verificari_VerificareId",
                table: "Spectacole");

            migrationBuilder.DropForeignKey(
                name: "FK_Vanzari_Verificari_VerificareId",
                table: "Vanzari");

            migrationBuilder.DropIndex(
                name: "IX_Vanzari_VerificareId",
                table: "Vanzari");

            migrationBuilder.DropIndex(
                name: "IX_Spectacole_VerificareId",
                table: "Spectacole");

            migrationBuilder.DropColumn(
                name: "VanzareId",
                table: "Verificari");

            migrationBuilder.DropColumn(
                name: "VerificareId",
                table: "Vanzari");

            migrationBuilder.DropColumn(
                name: "VerificareId",
                table: "Spectacole");

            migrationBuilder.AlterColumn<int>(
                name: "SpectacolId",
                table: "Verificari",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Verificari",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Verificari_SpectacolId",
                table: "Verificari",
                column: "SpectacolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Verificari_Spectacole_SpectacolId",
                table: "Verificari",
                column: "SpectacolId",
                principalTable: "Spectacole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Verificari_Spectacole_SpectacolId",
                table: "Verificari");

            migrationBuilder.DropIndex(
                name: "IX_Verificari_SpectacolId",
                table: "Verificari");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Verificari");

            migrationBuilder.AlterColumn<double>(
                name: "SpectacolId",
                table: "Verificari",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "VanzareId",
                table: "Verificari",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
    }
}
