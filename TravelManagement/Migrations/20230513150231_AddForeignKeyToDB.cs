using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirlineId",
                table: "Journeys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Journeys",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "AirlineId",
                table: "Journeys");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Journeys");
        }
    }
}
