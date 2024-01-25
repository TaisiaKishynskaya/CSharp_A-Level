using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CatalogItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8324));

            migrationBuilder.UpdateData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8327));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8341), 5 });

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8346), 10 });

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8348), 15 });

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8350), 6 });

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8352), 10 });

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8355), 2 });

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "Quantity" },
                values: new object[] { new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8357), 5 });

            migrationBuilder.UpdateData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8314));

            migrationBuilder.UpdateData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 25, 13, 6, 23, 110, DateTimeKind.Utc).AddTicks(8319));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CatalogItem");

            migrationBuilder.UpdateData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8725));

            migrationBuilder.UpdateData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8730));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8750));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8755));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8760));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8764));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8767));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8772));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8776));

            migrationBuilder.UpdateData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8712));

            migrationBuilder.UpdateData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 22, 10, 3, 22, 956, DateTimeKind.Utc).AddTicks(8719));
        }
    }
}
