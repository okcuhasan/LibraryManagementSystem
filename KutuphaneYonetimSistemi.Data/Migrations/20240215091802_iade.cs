using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneYonetimSistemi.Data.Migrations
{
    /// <inheritdoc />
    public partial class iade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Iade",
                columns: table => new
                {
                    IadeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OduncId = table.Column<int>(type: "int", nullable: true),
                    IadeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iade", x => x.IadeId);
                    table.ForeignKey(
                        name: "FK_Iade_Odunc_OduncId",
                        column: x => x.OduncId,
                        principalTable: "Odunc",
                        principalColumn: "OduncId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Iade_OduncId",
                table: "Iade",
                column: "OduncId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Iade");
        }
    }
}
