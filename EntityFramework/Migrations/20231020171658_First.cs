using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AcountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Acounts_AcountId",
                        column: x => x.AcountId,
                        principalTable: "Acounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incoums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AcountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incoums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incoums_Acounts_AcountId",
                        column: x => x.AcountId,
                        principalTable: "Acounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Costs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Acounts",
                columns: new[] { "Id", "Login", "Password", "Profit" },
                values: new object[] { 1, "qwerty", "qwerty-1", 0m });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "AcountId", "Name", "Summ" },
                values: new object[,]
                {
                    { 1, 1, "Food", 0m },
                    { 2, 1, "Cafe", 0m },
                    { 3, 1, "Entertainment", 0m },
                    { 4, 1, "Transport", 0m },
                    { 5, 1, "Health", 0m },
                    { 6, 1, "Pet", 0m },
                    { 7, 1, "Family", 0m },
                    { 8, 1, "Clothes", 0m }
                });

            migrationBuilder.InsertData(
                table: "Incoums",
                columns: new[] { "Id", "AcountId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Salary", 10200m },
                    { 2, 1, "Hospital", 3000m }
                });

            migrationBuilder.InsertData(
                table: "Costs",
                columns: new[] { "Id", "CategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Cheese", 85m },
                    { 2, 8, "Jacket", 2000m },
                    { 3, 5, "Vitamins", 500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AcountId",
                table: "Categories",
                column: "AcountId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CategoryId",
                table: "Costs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incoums_AcountId",
                table: "Incoums",
                column: "AcountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropTable(
                name: "Incoums");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Acounts");
        }
    }
}
