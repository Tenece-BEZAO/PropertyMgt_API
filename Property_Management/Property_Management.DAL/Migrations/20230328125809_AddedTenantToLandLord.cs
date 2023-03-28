using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    public partial class AddedTenantToLandLord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "LandLord");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "LandLord",
                newName: "PropertiesId");

            migrationBuilder.AddColumn<string>(
                name: "TenantsId",
                table: "LandLord",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantsId",
                table: "LandLord");

            migrationBuilder.RenameColumn(
                name: "PropertiesId",
                table: "LandLord",
                newName: "TenantId");

            migrationBuilder.AddColumn<string>(
                name: "PropertyId",
                table: "LandLord",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
