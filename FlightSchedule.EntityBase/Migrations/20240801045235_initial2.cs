using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSchedule.EntityBase.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "idx_subscription_agency_id",
                table: "Subscription",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "idx_subscription_destination_city_id",
                table: "Subscription",
                column: "DestinationCityId");

            migrationBuilder.CreateIndex(
                name: "idx_subscription_origin_city_id",
                table: "Subscription",
                column: "OriginCityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_subscription_agency_id",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "idx_subscription_destination_city_id",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "idx_subscription_origin_city_id",
                table: "Subscription");
        }
    }
}
