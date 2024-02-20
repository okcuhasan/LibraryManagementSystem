using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutuphaneYonetimSistemi.Data.Migrations
{
    /// <inheritdoc />
    public partial class yorumcevaplari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YorumCevaplari",
                columns: table => new
                {
                    CevapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CevapIcerigi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YorumId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YorumCevaplari", x => x.CevapId);
                    table.ForeignKey(
                        name: "FK_YorumCevaplari_AspNetUsers_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YorumCevaplari_Yorumlar_YorumId",
                        column: x => x.YorumId,
                        principalTable: "Yorumlar",
                        principalColumn: "YorumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YorumCevaplari_KullaniciId",
                table: "YorumCevaplari",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_YorumCevaplari_YorumId",
                table: "YorumCevaplari",
                column: "YorumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YorumCevaplari");
        }
    }
}
