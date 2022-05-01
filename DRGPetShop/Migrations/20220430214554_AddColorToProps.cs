using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DRGPetShop.MVC.Migrations
{
    public partial class AddColorToProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Behaviour",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Behaviour");
        }
    }
}
