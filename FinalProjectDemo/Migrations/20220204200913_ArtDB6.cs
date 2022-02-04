using Microsoft.EntityFrameworkCore.Migrations;

namespace FavoritesDemo.Migrations
{
    public partial class ArtDB6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtMiniDetails",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    thumbnail_url = table.Column<string>(nullable: true),
                    artist = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtMiniDetails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(nullable: true),
                    artwork_id = table.Column<int>(nullable: false),
                    mynotes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtMiniDetails");

            migrationBuilder.DropTable(
                name: "UserFavorites");
        }
    }
}
