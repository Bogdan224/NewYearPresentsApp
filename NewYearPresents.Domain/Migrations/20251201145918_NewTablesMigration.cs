using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewYearPresents.Domain.Migrations
{
    /// <inheritdoc />
    public partial class NewTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presents_Packaging_PackagingId",
                table: "Presents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packaging",
                table: "Packaging");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Presents");

            migrationBuilder.RenameTable(
                name: "Packaging",
                newName: "Packagings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packagings",
                table: "Packagings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackagingsInStorage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    PackagingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingsInStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackagingsInStorage_Packagings_PackagingId",
                        column: x => x.PackagingId,
                        principalTable: "Packagings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsBoxesInStorage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductsBoxId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsBoxesInStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsBoxesInStorage_ProductsBoxes_ProductsBoxId",
                        column: x => x.ProductsBoxId,
                        principalTable: "ProductsBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsInPresent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PresentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInPresent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsInPresent_Presents_PresentId",
                        column: x => x.PresentId,
                        principalTable: "Presents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInPresent_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PresentsInOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    PresentId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresentsInOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PresentsInOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PresentsInOrder_Presents_PresentId",
                        column: x => x.PresentId,
                        principalTable: "Presents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsBoxesInOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductsBoxId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsBoxesInOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsBoxesInOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsBoxesInOrder_ProductsBoxes_ProductsBoxId",
                        column: x => x.ProductsBoxId,
                        principalTable: "ProductsBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackagingsInStorage_PackagingId",
                table: "PackagingsInStorage",
                column: "PackagingId");

            migrationBuilder.CreateIndex(
                name: "IX_PresentsInOrder_OrderId",
                table: "PresentsInOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PresentsInOrder_PresentId",
                table: "PresentsInOrder",
                column: "PresentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsBoxesInOrder_OrderId",
                table: "ProductsBoxesInOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsBoxesInOrder_ProductsBoxId",
                table: "ProductsBoxesInOrder",
                column: "ProductsBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsBoxesInStorage_ProductsBoxId",
                table: "ProductsBoxesInStorage",
                column: "ProductsBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInPresent_PresentId",
                table: "ProductsInPresent",
                column: "PresentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInPresent_ProductId",
                table: "ProductsInPresent",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presents_Packagings_PackagingId",
                table: "Presents",
                column: "PackagingId",
                principalTable: "Packagings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presents_Packagings_PackagingId",
                table: "Presents");

            migrationBuilder.DropTable(
                name: "PackagingsInStorage");

            migrationBuilder.DropTable(
                name: "PresentsInOrder");

            migrationBuilder.DropTable(
                name: "ProductsBoxesInOrder");

            migrationBuilder.DropTable(
                name: "ProductsBoxesInStorage");

            migrationBuilder.DropTable(
                name: "ProductsInPresent");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packagings",
                table: "Packagings");

            migrationBuilder.RenameTable(
                name: "Packagings",
                newName: "Packaging");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Presents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packaging",
                table: "Packaging",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Presents_Packaging_PackagingId",
                table: "Presents",
                column: "PackagingId",
                principalTable: "Packaging",
                principalColumn: "Id");
        }
    }
}
