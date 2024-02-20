using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneYonetimSistemi.Data.Migrations
{
    /// <inheritdoc />
    public partial class iade2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IadeTarihi",
                table: "Odunc");

            migrationBuilder.AddColumn<int>(
                name: "KitapId",
                table: "Iade",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KullaniciId",
                table: "Iade",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Iade_KitapId",
                table: "Iade",
                column: "KitapId");

            migrationBuilder.CreateIndex(
                name: "IX_Iade_KullaniciId",
                table: "Iade",
                column: "KullaniciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Iade_AspNetUsers_KullaniciId",
                table: "Iade",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Iade_Kitaplar_KitapId",
                table: "Iade",
                column: "KitapId",
                principalTable: "Kitaplar",
                principalColumn: "KitapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Iade_AspNetUsers_KullaniciId",
                table: "Iade");

            migrationBuilder.DropForeignKey(
                name: "FK_Iade_Kitaplar_KitapId",
                table: "Iade");

            migrationBuilder.DropIndex(
                name: "IX_Iade_KitapId",
                table: "Iade");

            migrationBuilder.DropIndex(
                name: "IX_Iade_KullaniciId",
                table: "Iade");

            migrationBuilder.DropColumn(
                name: "KitapId",
                table: "Iade");

            migrationBuilder.DropColumn(
                name: "KullaniciId",
                table: "Iade");

            migrationBuilder.AddColumn<DateTime>(
                name: "IadeTarihi",
                table: "Odunc",
                type: "datetime2",
                nullable: true);
        }
    }
}
