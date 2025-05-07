using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class addedcitytype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityEnum",
                schema: "sr",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "sr",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 1,
                column: "CityEnum",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "sr",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 2,
                column: "CityEnum",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "sr",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 3,
                column: "CityEnum",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "sr",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 4,
                column: "CityEnum",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "sr",
                table: "Cities",
                keyColumn: "CityId",
                keyValue: 5,
                column: "CityEnum",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityEnum",
                schema: "sr",
                table: "Cities");
        }
    }
}
