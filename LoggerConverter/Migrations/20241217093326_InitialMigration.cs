using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LoggerConverter.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(maxLength: 200, nullable: false),
                    IsConverted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 278, DateTimeKind.Local)),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 279, DateTimeKind.Local))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogsConverted",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdLog = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 281, DateTimeKind.Local)),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2024, 12, 17, 6, 33, 26, 281, DateTimeKind.Local))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsConverted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogsConverted_Logs_IdLog",
                        column: x => x.IdLog,
                        principalTable: "Logs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogsConverted_IdLog",
                table: "LogsConverted",
                column: "IdLog",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogsConverted");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
