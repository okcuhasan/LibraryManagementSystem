using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneYonetimSistemi.Data.Migrations
{
    /// <inheritdoc />
    public partial class iliskieklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YayineviId",
                table: "Kitaplar",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YazarId",
                table: "Kitaplar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_YayineviId",
                table: "Kitaplar",
                column: "YayineviId");

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_YazarId",
                table: "Kitaplar",
                column: "YazarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitaplar_Yayinevleri_YayineviId",
                table: "Kitaplar",
                column: "YayineviId",
                principalTable: "Yayinevleri",
                principalColumn: "YayinEviId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitaplar_Yazarlar_YazarId",
                table: "Kitaplar",
                column: "YazarId",
                principalTable: "Yazarlar",
                principalColumn: "YazarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Yayinevleri_YayineviId",
                table: "Kitaplar");

            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Yazarlar_YazarId",
                table: "Kitaplar");

            migrationBuilder.DropIndex(
                name: "IX_Kitaplar_YayineviId",
                table: "Kitaplar");

            migrationBuilder.DropIndex(
                name: "IX_Kitaplar_YazarId",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "YayineviId",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "YazarId",
                table: "Kitaplar");
        }
    }
}
