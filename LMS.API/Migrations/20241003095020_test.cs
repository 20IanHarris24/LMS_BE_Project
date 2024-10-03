using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.API.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("17a4e6bd-dc57-47fb-9843-f2c01192520c"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("27c64684-3c5f-4b60-a567-eb1611f54ca8"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("43a75c47-4f68-4b19-9117-28768c8fca6c"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("5f3d56fd-8b39-44ce-943c-2e316532813d"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("6f01e571-41f0-4789-8059-422ae07d736e"),
                column: "Start",
                value: new DateTime(2024, 10, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("a767cdee-e833-427a-9349-3ee71cca8a39"),
                column: "Start",
                value: new DateTime(2024, 10, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9535));

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "CourseId", "Description", "End", "Name", "Start" },
                values: new object[,]
                {
                    { new Guid("45d6a92b-b101-4a9a-95cf-c0a178c07434"), new Guid("6f01e571-41f0-4789-8059-422ae07d736e"), "Intro to Polynomials", new DateTime(2024, 12, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9729), "Polynomials", new DateTime(2024, 11, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9728) },
                    { new Guid("885bdc5a-5d24-4f44-b5b3-9150bc56e0de"), new Guid("6f01e571-41f0-4789-8059-422ae07d736e"), "Intro to Functions", new DateTime(2024, 11, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9719), "Functions", new DateTime(2024, 10, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9715) },
                    { new Guid("ab331f58-9288-44c4-b9fc-beeeaa1e69bd"), new Guid("a767cdee-e833-427a-9349-3ee71cca8a39"), "Intro to Kinematics", new DateTime(2024, 12, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9734), "Kimenatics", new DateTime(2024, 11, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9734) },
                    { new Guid("cca28c92-9699-4563-8de4-260d748db7f0"), new Guid("a767cdee-e833-427a-9349-3ee71cca8a39"), "Intro to Vektors", new DateTime(2024, 11, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9732), "Vektors", new DateTime(2024, 10, 3, 9, 50, 18, 133, DateTimeKind.Utc).AddTicks(9731) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("45d6a92b-b101-4a9a-95cf-c0a178c07434"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("885bdc5a-5d24-4f44-b5b3-9150bc56e0de"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("ab331f58-9288-44c4-b9fc-beeeaa1e69bd"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cca28c92-9699-4563-8de4-260d748db7f0"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("6f01e571-41f0-4789-8059-422ae07d736e"),
                column: "Start",
                value: new DateTime(2024, 10, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3136));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("a767cdee-e833-427a-9349-3ee71cca8a39"),
                column: "Start",
                value: new DateTime(2024, 10, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3138));

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "CourseId", "Description", "End", "Name", "Start" },
                values: new object[,]
                {
                    { new Guid("17a4e6bd-dc57-47fb-9843-f2c01192520c"), new Guid("a767cdee-e833-427a-9349-3ee71cca8a39"), "Intro to Vektors", new DateTime(2024, 11, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3305), "Vektors", new DateTime(2024, 10, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3305) },
                    { new Guid("27c64684-3c5f-4b60-a567-eb1611f54ca8"), new Guid("6f01e571-41f0-4789-8059-422ae07d736e"), "Intro to Functions", new DateTime(2024, 11, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3295), "Functions", new DateTime(2024, 10, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3292) },
                    { new Guid("43a75c47-4f68-4b19-9117-28768c8fca6c"), new Guid("a767cdee-e833-427a-9349-3ee71cca8a39"), "Intro to Kinematics", new DateTime(2024, 12, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3307), "Kimenatics", new DateTime(2024, 11, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3307) },
                    { new Guid("5f3d56fd-8b39-44ce-943c-2e316532813d"), new Guid("6f01e571-41f0-4789-8059-422ae07d736e"), "Intro to Polynomials", new DateTime(2024, 12, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3303), "Polynomials", new DateTime(2024, 11, 2, 17, 27, 29, 869, DateTimeKind.Utc).AddTicks(3302) }
                });
        }
    }
}
