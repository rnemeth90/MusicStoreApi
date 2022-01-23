using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicApi.Migrations
{
    public partial class UpdateAlbumWithSongs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Songs");
        }
    }
}
