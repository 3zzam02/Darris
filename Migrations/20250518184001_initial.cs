using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestStoreMVC.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Imagess");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Imagess");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Imagess",
                newName: "Url");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Imagess",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Imagess",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Imagess",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ImageModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageModels_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imagess_ProductId",
                table: "Imagess",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageModels_ProductId",
                table: "ImageModels",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Imagess_Products_ProductId",
                table: "Imagess",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagess_Products_ProductId",
                table: "Imagess");

            migrationBuilder.DropTable(
                name: "ImageModels");

            migrationBuilder.DropIndex(
                name: "IX_Imagess_ProductId",
                table: "Imagess");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Imagess");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Imagess");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Imagess",
                newName: "ImagePath");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Imagess",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Imagess",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Imagess",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
