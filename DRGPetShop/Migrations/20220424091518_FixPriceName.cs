using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DRGPetShop.MVC.Migrations
{
    public partial class FixPriceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pirce",
                table: "Product",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Product",
                newName: "Pirce");
        }
    }
}
