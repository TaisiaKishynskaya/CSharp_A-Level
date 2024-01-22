using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7630));

            migrationBuilder.UpdateData(
                table: "CatalogBrand",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7633));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7643));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7648));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7650));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7652));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7654));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7658));

            migrationBuilder.UpdateData(
                table: "CatalogItem",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7708));

            migrationBuilder.UpdateData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7623));

            migrationBuilder.UpdateData(
                table: "CatalogType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 20, 22, 27, 4, 896, DateTimeKind.Utc).AddTicks(7627));
        }
    }
}
