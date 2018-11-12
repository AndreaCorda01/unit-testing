using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoUnitTesting.Core.DataAccess.Migrations
{
    public partial class PosterRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PosterPath",
                table: "Movies",
                newName: "Poster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Poster",
                table: "Movies",
                newName: "PosterPath");
        }
    }
}
