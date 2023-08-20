using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Restaurant = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    last_modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    bio = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Slug = table.Column<string>(type: "TEXT", nullable: true),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    last_modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Promotiont",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    promotional_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    last_modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotiont", x => x.id);
                    table.ForeignKey(
                        name: "FK_Promotiont_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    UserEntityId = table.Column<int>(type: "integer", nullable: true),
                    created = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    last_modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.id);
                    table.ForeignKey(
                        name: "FK_Restaurant_User_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_id",
                table: "Product",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Promotiont_id",
                table: "Promotiont",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Promotiont_ProductId",
                table: "Promotiont",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_id",
                table: "Restaurant",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_UserEntityId",
                table: "Restaurant",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_User_id",
                table: "User",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promotiont");

            migrationBuilder.DropTable(
                name: "Restaurant");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
