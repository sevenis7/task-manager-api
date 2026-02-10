using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateWithSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "🏠", "Home" },
                    { 2, "💼", "Work" },
                    { 3, "🎓", "Study" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "DueDate", "IsCompleted", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "1st task description", new DateTime(2026, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "My 1st task" },
                    { 2, 1, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "2nd task description", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "My 2nd task" },
                    { 3, 2, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "3rd task description", new DateTime(2026, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "My 3rd task" },
                    { 4, 2, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "4th task description", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "My 4th task" },
                    { 5, 3, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "5th task description", new DateTime(2026, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "My 5th task" },
                    { 6, 3, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "6th task description", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "My 6th task" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
