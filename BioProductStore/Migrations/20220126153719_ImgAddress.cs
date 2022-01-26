using Microsoft.EntityFrameworkCore.Migrations;

namespace BioProductStore.Migrations
{
    public partial class ImgAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imgAddress",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgAddress",
                table: "Categories");
        }
    }
}
