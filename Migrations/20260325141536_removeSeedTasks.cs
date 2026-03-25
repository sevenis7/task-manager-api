using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class removeSeedTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 6);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "DueDate", "Name", "PriorityId", "StatusId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "1st task description", new DateTime(2026, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "My 1st task", 2, 1, 0 },
                    { 2, 1, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "2nd task description", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "My 2nd task", 2, 1, 0 },
                    { 3, 2, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "3rd task description", new DateTime(2026, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "My 3rd task", 2, 1, 0 },
                    { 4, 2, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "4th task description", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "My 4th task", 2, 1, 0 },
                    { 5, 3, new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "5th task description", new DateTime(2026, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "My 5th task", 2, 1, 0 },
                    { 6, 3, new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "6th task description", new DateTime(2025, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "My 6th task", 2, 1, 0 }
                });
        }
    }
}
