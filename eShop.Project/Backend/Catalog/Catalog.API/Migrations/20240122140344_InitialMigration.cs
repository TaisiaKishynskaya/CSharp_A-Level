using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PictureFile = table.Column<string>(type: "text", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItem_CatalogBrand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "CatalogBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItem_CatalogType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CatalogType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CatalogBrand",
                columns: new[] { "Id", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8198), "Nike", null },
                    { 2, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8200), "Adidas", null }
                });

            migrationBuilder.InsertData(
                table: "CatalogType",
                columns: new[] { "Id", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8190), "Shoes", null },
                    { 2, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8194), "Hoodie", null }
                });

            migrationBuilder.InsertData(
                table: "CatalogItem",
                columns: new[] { "Id", "BrandId", "CreatedAt", "Description", "PictureFile", "Price", "Quantity", "Title", "TypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8208), "Created for the hardwood but taken to the streets, the '80s b-ball icon returns with classic details and throwback hoops flair. ", "nike-dunk-low-retro-premium.png", 100m, 5, "Nike Dunk Low Retro Premium", 1, null },
                    { 2, 1, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8211), "Created for the hardwood but taken to the streets, the '80s b-ball icon returns with crisp leather and and classic \"Panda\" color blocking.", "nike-dunk-mid.png", 120m, 10, "Nike Dunk Mid", 1, null },
                    { 3, 1, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8212), "Meet your new go-to hoodie. Heavyweight fleece feels super soft, and the comfy, relaxed fit will have you reaching for it again and again.", "jordan-flight-fleece.png", 100m, 15, "Jordan Flight Fleece", 2, null },
                    { 4, 2, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8214), "These are not just sneakers, but a real symbol of the era. The adidas Forum model appeared in 1984 and won love not only on basketball courts, but also in show business.", "forum-low.png", 110m, 6, "Forum Low", 1, null },
                    { 5, 2, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8216), "For your team. For your planet. Created with grassroots football in mind, the Entrada 22 range gives you everything you need to make your game feel and look more beautiful. ", "entrada-22-sweat-hoodie.png", 60m, 10, "ENTRADA 22 SWEAT HOODIE", 2, null },
                    { 6, 1, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8219), "Ja Morant became the superstar he is today by repeatedly sinking jumpers on crooked rims, jumping on tractor tires and dribbling through traffic cones in steamy South Carolina summers.", "ja-1.png", 130m, 2, "Ja 1", 1, null },
                    { 7, 1, new DateTime(2024, 1, 22, 14, 3, 43, 830, DateTimeKind.Utc).AddTicks(8220), "Feel unbeatable, from the tee box to the final putt. Inspired by one of the most iconic sneakers of all time, the Air Jordan 1 G is an instant classic on the course. ", "air-jordan-i-high.png", 190m, 5, "Air Jordan I High G", 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItem_BrandId",
                table: "CatalogItem",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItem_TypeId",
                table: "CatalogItem",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItem");

            migrationBuilder.DropTable(
                name: "CatalogBrand");

            migrationBuilder.DropTable(
                name: "CatalogType");
        }
    }
}
