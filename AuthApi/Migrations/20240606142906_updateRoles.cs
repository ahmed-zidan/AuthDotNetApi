using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthApi.Migrations
{
    public partial class updateRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HaveRead",
                table: "RolePermissions",
                newName: "HaveAdd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HaveAdd",
                table: "RolePermissions",
                newName: "HaveRead");
        }
    }
}
