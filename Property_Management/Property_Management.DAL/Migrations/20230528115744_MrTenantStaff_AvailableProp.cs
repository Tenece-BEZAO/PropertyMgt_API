using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MrTenantStaff_AvailableProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_Units_PropertyId",
                table: "Units");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Tenants_RequestedBy",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_Units_PropertyId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Available",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "RequestedBy",
                table: "MaintenanceRequests",
                newName: "LoggedBy");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceRequests_RequestedBy",
                table: "MaintenanceRequests",
                newName: "IX_MaintenanceRequests_LoggedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Units_PropertyId",
                table: "Units",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Tenants_LoggedBy",
                table: "MaintenanceRequests",
                column: "LoggedBy",
                principalTable: "Tenants",
                principalColumn: "TenantId");
        }
    }
}
