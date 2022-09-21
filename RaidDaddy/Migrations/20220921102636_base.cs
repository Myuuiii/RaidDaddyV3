using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaidDaddy.Migrations
{
    public partial class @base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fireteams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Raid = table.Column<int>(type: "int", nullable: false),
                    Encounter = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fireteams", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Raiders",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Class = table.Column<int>(type: "int", nullable: false),
                    Subclass = table.Column<int>(type: "int", nullable: false),
                    CurrentTeamId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raiders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raiders_Fireteams_CurrentTeamId",
                        column: x => x.CurrentTeamId,
                        principalTable: "Fireteams",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Raiders_CurrentTeamId",
                table: "Raiders",
                column: "CurrentTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Raiders");

            migrationBuilder.DropTable(
                name: "Fireteams");
        }
    }
}
