using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelManagement.Migrations
{
    /// <inheritdoc />
    public partial class @as : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Airlines_AirlineId",
                table: "Journeys");

            migrationBuilder.DropForeignKey(
                name: "FK_Journeys_Flights_FlightId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Journeys_AirlineId",
                table: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Journeys_FlightId",
                table: "Journeys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Journeys_AirlineId",
                table: "Journeys",
                column: "AirlineId");

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_FlightId",
                table: "Journeys",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Airlines_AirlineId",
                table: "Journeys",
                column: "AirlineId",
                principalTable: "Airlines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Journeys_Flights_FlightId",
                table: "Journeys",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
