using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DRGPetShop.MVC.Migrations
{
    public partial class AddBehaviourToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BehaviourId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_BehaviourId",
                table: "Product",
                column: "BehaviourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Behaviour_BehaviourId",
                table: "Product",
                column: "BehaviourId",
                principalTable: "Behaviour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Behaviour_BehaviourId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BehaviourId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BehaviourId",
                table: "Product");
        }
    }
}
