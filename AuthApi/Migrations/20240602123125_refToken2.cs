using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthApi.Migrations
{
    public partial class refToken2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                 name: "RefreshTokens");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "RefreshTokens",
                 columns: table => new
                 {
                     Id = table.Column<int>(type: "int", nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                     TokenId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                     RefreshTokenId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                 });
        }
    }
}
