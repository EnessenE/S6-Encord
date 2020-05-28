using Microsoft.EntityFrameworkCore.Migrations;

namespace Encord.GuildService.Migrations
{
    public partial class NonDeletableGuilds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deletable",
                table: "Guilds",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deletable",
                table: "Guilds");
        }
    }
}
