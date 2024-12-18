using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoggerConverter.Migrations
{
    public partial class AddPathToConvert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "LogsConverted",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 281, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LogsConverted",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 281, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "LogsConverted",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Logs",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 279, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Logs",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 278, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "LogsConverted");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "LogsConverted",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 281, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "LogsConverted",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 281, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 279, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 278, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
