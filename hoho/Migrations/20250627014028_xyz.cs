using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hoho.Migrations
{
    /// <inheritdoc />
    public partial class xyz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UnitConversions_FatherId",
                table: "UnitConversions");

            migrationBuilder.CreateIndex(
                name: "IX_UnitConversions_FatherId_AlternateUoMId",
                table: "UnitConversions",
                columns: new[] { "FatherId", "AlternateUoMId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UnitConversions_FatherId_AlternateUoMId",
                table: "UnitConversions");

            migrationBuilder.CreateIndex(
                name: "IX_UnitConversions_FatherId",
                table: "UnitConversions",
                column: "FatherId");
        }
    }
}
