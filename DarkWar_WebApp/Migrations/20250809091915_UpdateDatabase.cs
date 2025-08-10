using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarkWar_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Players_PlayerID",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerID",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CPEntries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerID = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Value = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPEntries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CPEntries_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CPEntries_PlayerID",
                table: "CPEntries",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_PlayerID",
                table: "Events",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Players_PlayerID",
                table: "Events");

            migrationBuilder.DropTable(
                name: "CPEntries");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerID",
                table: "Events",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Players_PlayerID",
                table: "Events",
                column: "PlayerID",
                principalTable: "Players",
                principalColumn: "ID");
        }
    }
}
