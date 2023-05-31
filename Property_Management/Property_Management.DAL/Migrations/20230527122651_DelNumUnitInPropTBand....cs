using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DelNumUnitInPropTBand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfUnits",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Units",
                newName: "TenantsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantsId",
                table: "Units",
                newName: "TenantId");

            migrationBuilder.AddColumn<string>(
                name: "NumOfUnits",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
