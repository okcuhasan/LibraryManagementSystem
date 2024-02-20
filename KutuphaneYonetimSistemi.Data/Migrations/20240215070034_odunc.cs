using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneYonetimSistemi.Data.Migrations
{
    /// <inheritdoc />
    public partial class odunc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isOduncAlindi",
                table: "Kitaplar");

            migrationBuilder.CreateTable(
                name: "Odunc",
                columns: table => new
                {
                    OduncId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KitapId = table.Column<int>(type: "int", nullable: true),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OduncAlmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IadeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odunc", x => x.OduncId);
                    table.ForeignKey(
                        name: "FK_Odunc_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Odunc_Kitaplar_KitapId",
                        column: x => x.KitapId,
                        principalTable: "Kitaplar",
                        principalColumn: "KitapId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Odunc_KitapId",
                table: "Odunc",
                column: "KitapId");

            migrationBuilder.CreateIndex(
                name: "IX_Odunc_KullaniciId",
                table: "Odunc",
                column: "KullaniciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odunc");

            migrationBuilder.AddColumn<bool>(
                name: "isOduncAlindi",
                table: "Kitaplar",
                type: "bit",
                nullable: true);
        }
    }
}
