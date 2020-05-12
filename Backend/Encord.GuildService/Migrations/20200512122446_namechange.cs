using Microsoft.EntityFrameworkCore.Migrations;

namespace Encord.GuildService.Migrations
{
    public partial class namechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Guilds",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Guilds");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Guilds",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guilds",
                table: "Guilds",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Guilds",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Guilds");

            migrationBuilder.AddColumn<string>(
                name: "GuildId",
                table: "Guilds",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guilds",
                table: "Guilds",
                column: "GuildId");
        }
    }
}
