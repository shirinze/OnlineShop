using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class addedisrequiredfirstname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                schema: "sr",
                table: "UserEntities",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "sr",
                table: "UserEntities",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "sr",
                table: "UserEntities");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                schema: "sr",
                table: "UserEntities",
                newName: "UpdateAt");
        }
    }
}
