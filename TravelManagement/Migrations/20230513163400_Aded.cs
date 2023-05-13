using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelManagement.Migrations
{
    /// <inheritdoc />
    public partial class Aded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Journeys_FlightId",
                table: "Journeys",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Flights_FlightId",
                table: "Journeys",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Flights_FlightId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Journeys_FlightId",
                table: "Journeys");
        }
    }
}
