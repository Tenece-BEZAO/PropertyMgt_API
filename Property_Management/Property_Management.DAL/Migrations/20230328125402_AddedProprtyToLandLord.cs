using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    public partial class AddedProprtyToLandLord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_LandLord_LandLordId",
                table: "Tenants");

            migrationBuilder.AlterColumn<string>(
                name: "LandLordId",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_LandLord_LandLordId",
                table: "Tenants",
                column: "LandLordId",
                principalTable: "LandLord",
                principalColumn: "LandLordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_LandLord_LandLordId",
                table: "Tenants");

            migrationBuilder.AlterColumn<string>(
                name: "LandLordId",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_LandLord_LandLordId",
                table: "Tenants",
                column: "LandLordId",
                principalTable: "LandLord",
                principalColumn: "LandLordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
