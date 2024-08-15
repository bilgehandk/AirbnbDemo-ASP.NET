using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addnewproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_CalenderAvailability_CalenderAvailabilityId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_CalenderAvailabilityId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "CalenderAvailabilityId",
                table: "Property");

            migrationBuilder.AddColumn<int>(
                name: "PropertyInfoId",
                table: "CalenderAvailability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CalenderAvailability_PropertyInfoId",
                table: "CalenderAvailability",
                column: "PropertyInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderAvailability_Property_PropertyInfoId",
                table: "CalenderAvailability",
                column: "PropertyInfoId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalenderAvailability_Property_PropertyInfoId",
                table: "CalenderAvailability");

            migrationBuilder.DropIndex(
                name: "IX_CalenderAvailability_PropertyInfoId",
                table: "CalenderAvailability");

            migrationBuilder.DropColumn(
                name: "PropertyInfoId",
                table: "CalenderAvailability");

            migrationBuilder.AddColumn<int>(
                name: "CalenderAvailabilityId",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Property_CalenderAvailabilityId",
                table: "Property",
                column: "CalenderAvailabilityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_CalenderAvailability_CalenderAvailabilityId",
                table: "Property",
                column: "CalenderAvailabilityId",
                principalTable: "CalenderAvailability",
                principalColumn: "CalenderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
