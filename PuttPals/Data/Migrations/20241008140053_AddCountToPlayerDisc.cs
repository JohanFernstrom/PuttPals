using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuttPals.Migrations
{
    /// <inheritdoc />
    public partial class AddCountToPlayerDisc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "PlayerDiscs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "PlayerDiscs");
        }
    }
}
