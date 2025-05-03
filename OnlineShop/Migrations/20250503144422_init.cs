using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sr");

            migrationBuilder.CreateSequence<int>(
                name: "Increase3By3",
                schema: "sr",
                startValue: 10L,
                incrementBy: 3);

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "sr",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "UserEntities",
                schema: "sr",
                columns: table => new
                {
                    UserEntityId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR sr.Increase3By3"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.UserEntityId);
                });

            migrationBuilder.InsertData(
                schema: "sr",
                table: "Cities",
                columns: new[] { "CityId", "CityName" },
                values: new object[,]
                {
                    { 1, "Tehran" },
                    { 2, "Mashhad" },
                    { 3, "Tabriz" },
                    { 4, "Uromiyeh" },
                    { 5, "Ahvaz" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities",
                schema: "sr");

            migrationBuilder.DropTable(
                name: "UserEntities",
                schema: "sr");

            migrationBuilder.DropSequence(
                name: "Increase3By3",
                schema: "sr");
        }
    }
}
