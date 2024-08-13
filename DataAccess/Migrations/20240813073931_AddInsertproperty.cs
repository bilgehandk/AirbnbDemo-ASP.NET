using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddInsertproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalenderAvailability_Property_PropertyId",
                table: "CalenderAvailability");

            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Property_PropertyId",
                table: "Fee");

            migrationBuilder.DropIndex(
                name: "IX_Fee_PropertyId",
                table: "Fee");

            migrationBuilder.DropIndex(
                name: "IX_CalenderAvailability_PropertyId",
                table: "CalenderAvailability");

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "Property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PropertyInfoId",
                table: "Fee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyInfoId",
                table: "CalenderAvailability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fee_PropertyInfoId",
                table: "Fee",
                column: "PropertyInfoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Property_PropertyInfoId",
                table: "Fee",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Property_PropertyInfoId",
                table: "Fee");

            migrationBuilder.DropIndex(
                name: "IX_Fee_PropertyInfoId",
                table: "Fee");

            migrationBuilder.DropIndex(
                name: "IX_CalenderAvailability_PropertyInfoId",
                table: "CalenderAvailability");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyInfoId",
                table: "Fee");

            migrationBuilder.DropColumn(
                name: "PropertyInfoId",
                table: "CalenderAvailability");

            migrationBuilder.CreateIndex(
                name: "IX_Fee_PropertyId",
                table: "Fee",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_CalenderAvailability_PropertyId",
                table: "CalenderAvailability",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalenderAvailability_Property_PropertyId",
                table: "CalenderAvailability",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Property_PropertyId",
                table: "Fee",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
