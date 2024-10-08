using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuttPals.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disc_Bag_BagId",
                table: "Disc");

            migrationBuilder.DropForeignKey(
                name: "FK_Disc_Players_PlayerId",
                table: "Disc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disc",
                table: "Disc");

            migrationBuilder.RenameTable(
                name: "Disc",
                newName: "Discs");

            migrationBuilder.RenameIndex(
                name: "IX_Disc_PlayerId",
                table: "Discs",
                newName: "IX_Discs_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Disc_BagId",
                table: "Discs",
                newName: "IX_Discs_BagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discs",
                table: "Discs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discs_Bag_BagId",
                table: "Discs",
                column: "BagId",
                principalTable: "Bag",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discs_Players_PlayerId",
                table: "Discs",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discs_Bag_BagId",
                table: "Discs");

            migrationBuilder.DropForeignKey(
                name: "FK_Discs_Players_PlayerId",
                table: "Discs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discs",
                table: "Discs");

            migrationBuilder.RenameTable(
                name: "Discs",
                newName: "Disc");

            migrationBuilder.RenameIndex(
                name: "IX_Discs_PlayerId",
                table: "Disc",
                newName: "IX_Disc_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Discs_BagId",
                table: "Disc",
                newName: "IX_Disc_BagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disc",
                table: "Disc",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Disc_Bag_BagId",
                table: "Disc",
                column: "BagId",
                principalTable: "Bag",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Disc_Players_PlayerId",
                table: "Disc",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
