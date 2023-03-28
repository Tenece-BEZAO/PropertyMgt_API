using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    public partial class DataTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Role_RoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_InspectionChecks_Units_UnitsUnitId",
                table: "InspectionChecks");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_InspectionChecks_UnitsUnitId",
                table: "InspectionChecks");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UnitsUnitId",
                table: "InspectionChecks");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TenantsId",
                table: "LandLord",
                newName: "TenantId");

            migrationBuilder.RenameColumn(
                name: "PropertiesId",
                table: "LandLord",
                newName: "PropertyId");

            migrationBuilder.AlterColumn<string>(
                name: "UnitId",
                table: "InspectionChecks",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NoOfUnits",
                table: "InspectionChecks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionChecks_UnitId",
                table: "InspectionChecks",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionChecks_Units_UnitId",
                table: "InspectionChecks",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionChecks_Units_UnitId",
                table: "InspectionChecks");

            migrationBuilder.DropIndex(
                name: "IX_InspectionChecks_UnitId",
                table: "InspectionChecks");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "LandLord",
                newName: "TenantsId");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "LandLord",
                newName: "PropertiesId");

            migrationBuilder.AlterColumn<int>(
                name: "UnitId",
                table: "InspectionChecks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfUnits",
                table: "InspectionChecks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UnitsUnitId",
                table: "InspectionChecks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InspectionChecks_UnitsUnitId",
                table: "InspectionChecks",
                column: "UnitsUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Role_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionChecks_Units_UnitsUnitId",
                table: "InspectionChecks",
                column: "UnitsUnitId",
                principalTable: "Units",
                principalColumn: "UnitId");
        }
    }
}
