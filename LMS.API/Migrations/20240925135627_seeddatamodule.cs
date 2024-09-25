using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.API.Migrations
{
    /// <inheritdoc />
    public partial class seeddatamodule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36c966c1-3e90-4ffc-88e5-f5920c427604");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "555426ef-24d0-467a-8961-749fe33453ea");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "284ebc37-63e1-4420-9a34-24ced1619797");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d556dbc4-d1d2-48c4-a6dc-f110d6b7297f");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("260b77ed-0918-413a-939d-0fcd5296f74d"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("b1eae6e6-de01-466e-a922-10d13dd45944"));

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Name", "Start" },
                values: new object[,]
                {
                    { new Guid("7e75b6fd-bf4e-4e1d-8fcd-32d9e73e7e12"), "Intro to Math", "Mathematics 101", new DateTime(2024, 9, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(6627) },
                    { new Guid("a70cfb21-2385-44e3-8a3d-bc35bd587c50"), "Intro to Physics", "Physics 101", new DateTime(2024, 9, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(6630) }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "CourseId", "Description", "End", "Name", "Start" },
                values: new object[,]
                {
                    { new Guid("68fb0290-b33c-4e7d-b8c2-2bacc4e66885"), new Guid("7e75b6fd-bf4e-4e1d-8fcd-32d9e73e7e12"), "Intro to Functions", new DateTime(2024, 10, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(7009), "Functions", new DateTime(2024, 9, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(7004) },
                    { new Guid("cf0fa898-6d14-466d-bea2-4da1caadfb61"), new Guid("7e75b6fd-bf4e-4e1d-8fcd-32d9e73e7e12"), "Intro to Polynomials", new DateTime(2024, 11, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(7021), "Polynomials", new DateTime(2024, 10, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(7020) },
                    { new Guid("d37c9443-8fa1-4bb9-80c6-a84e781c18c3"), new Guid("a70cfb21-2385-44e3-8a3d-bc35bd587c50"), "Intro to Vektors", new DateTime(2024, 10, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(7025), "Vektors", new DateTime(2024, 9, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(7024) },
                    { new Guid("f6acdb61-6751-4ff0-8cec-8093a2d272a3"), new Guid("a70cfb21-2385-44e3-8a3d-bc35bd587c50"), "Intro to Kinematics", new DateTime(2024, 11, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(7048), "Kimenatics", new DateTime(2024, 10, 25, 13, 56, 23, 810, DateTimeKind.Utc).AddTicks(7047) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("68fb0290-b33c-4e7d-b8c2-2bacc4e66885"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("cf0fa898-6d14-466d-bea2-4da1caadfb61"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("d37c9443-8fa1-4bb9-80c6-a84e781c18c3"));

            migrationBuilder.DeleteData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: new Guid("f6acdb61-6751-4ff0-8cec-8093a2d272a3"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("7e75b6fd-bf4e-4e1d-8fcd-32d9e73e7e12"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("a70cfb21-2385-44e3-8a3d-bc35bd587c50"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36c966c1-3e90-4ffc-88e5-f5920c427604", null, "Student", "STUDENT" },
                    { "555426ef-24d0-467a-8961-749fe33453ea", null, "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Name", "Start" },
                values: new object[,]
                {
                    { new Guid("260b77ed-0918-413a-939d-0fcd5296f74d"), "Intro to Math", "Mathematics 101", new DateTime(2024, 9, 24, 10, 56, 12, 525, DateTimeKind.Utc).AddTicks(8828) },
                    { new Guid("b1eae6e6-de01-466e-a922-10d13dd45944"), "Intro to Physics", "Physics 101", new DateTime(2024, 9, 24, 10, 56, 12, 525, DateTimeKind.Utc).AddTicks(8831) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CourseId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpireTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "284ebc37-63e1-4420-9a34-24ced1619797", 0, "9d9cb635-07b9-4807-a14f-8e3db55d3abf", new Guid("b1eae6e6-de01-466e-a922-10d13dd45944"), "student2@example.com", true, false, null, "STUDENT2@EXAMPLE.COM", "STUDENT2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMl8OA3nGQR/Xg6vitREFVF8PQzMmqANmWp4yp/yUh4ffHIASvluxe2gHq78Sz12FA==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "26cef1c5-c20b-46b1-b7fa-b74f357942fd", false, "student2@example.com" },
                    { "d556dbc4-d1d2-48c4-a6dc-f110d6b7297f", 0, "46339dea-9f11-4e85-bc7a-f1acbe8f3c76", new Guid("260b77ed-0918-413a-939d-0fcd5296f74d"), "student1@example.com", true, false, null, "STUDENT1@EXAMPLE.COM", "STUDENT1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHU/8IRpxJ/hk3zj0OAFQeGI6JtetjRchdAlyicMkars0ulrvi0ArZ5r4Zxv7jxk+A==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e9f44dd9-985a-46af-acdf-f24e85beaf58", false, "student1@example.com" }
                });
        }
    }
}
