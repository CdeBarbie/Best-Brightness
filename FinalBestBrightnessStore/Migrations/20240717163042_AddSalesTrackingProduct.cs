using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBestBrightnessStore.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesTrackingProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prodId",
                table: "SaleTrack");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateOfSale",
                table: "SaleTrack",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SalesTrackingProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    salesTrackId = table.Column<int>(type: "int", nullable: false),
                    prodId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesTrackingProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesTrackingProducts_SaleTrack_salesTrackId",
                        column: x => x.salesTrackId,
                        principalTable: "SaleTrack",
                        principalColumn: "salesTrackId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SalesTrackingProducts_salesTrackId",
                table: "SalesTrackingProducts",
                column: "salesTrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesTrackingProducts");

            migrationBuilder.AlterColumn<string>(
                name: "dateOfSale",
                table: "SaleTrack",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "prodId",
                table: "SaleTrack",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
