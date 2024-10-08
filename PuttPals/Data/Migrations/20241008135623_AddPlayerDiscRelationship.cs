using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuttPals.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerDiscRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discs_Players_PlayerId",
                table: "Discs");

            migrationBuilder.DropIndex(
                name: "IX_Discs_PlayerId",
                table: "Discs");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Discs");

            migrationBuilder.CreateTable(
                name: "PlayerDiscs",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    DiscId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerDiscs", x => new { x.PlayerId, x.DiscId });
                    table.ForeignKey(
                        name: "FK_PlayerDiscs_Discs_DiscId",
                        column: x => x.DiscId,
                        principalTable: "Discs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerDiscs_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDiscs_DiscId",
                table: "PlayerDiscs",
                column: "DiscId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerDiscs");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Discs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Discs_PlayerId",
                table: "Discs",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discs_Players_PlayerId",
                table: "Discs",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
