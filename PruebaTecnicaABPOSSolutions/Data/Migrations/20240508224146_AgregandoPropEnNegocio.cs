using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnicaABPOSSolutions.Data.Migrations
{
    public partial class AgregandoPropEnNegocio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Negocios",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Negocios_UserId",
                table: "Negocios",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Negocios_AspNetUsers_UserId",
                table: "Negocios",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Negocios_AspNetUsers_UserId",
                table: "Negocios");

            migrationBuilder.DropIndex(
                name: "IX_Negocios_UserId",
                table: "Negocios");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Negocios");
        }
    }
}
