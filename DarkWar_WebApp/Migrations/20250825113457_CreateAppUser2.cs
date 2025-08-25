using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarkWar_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateAppUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "AppUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AppUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
