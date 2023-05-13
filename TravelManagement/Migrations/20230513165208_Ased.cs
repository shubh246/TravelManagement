using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelManagement.Migrations
{
    /// <inheritdoc />
    public partial class Ased : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Journeys_AirlineId",
                table: "Journeys",
                column: "AirlineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Airlines_AirlineId",
                table: "Journeys",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Airlines_AirlineId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Journeys_AirlineId",
                table: "Journeys");
        }
    }
}
