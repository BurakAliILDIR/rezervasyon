using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rezervasyon.API.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trenler",
                columns: table => new
                {
                    Ad = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trenler", x => x.Ad);
                });

            migrationBuilder.CreateTable(
                name: "Vagonlar",
                columns: table => new
                {
                    Ad = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrenAd = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Kapasite = table.Column<int>(type: "int", nullable: false),
                    DoluKoltukAdet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vagonlar", x => x.Ad);
                    table.ForeignKey(
                        name: "FK_Vagonlar_Trenler_TrenAd",
                        column: x => x.TrenAd,
                        principalTable: "Trenler",
                        principalColumn: "Ad",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vagonlar_TrenAd",
                table: "Vagonlar",
                column: "TrenAd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vagonlar");

            migrationBuilder.DropTable(
                name: "Trenler");
        }
    }
}
