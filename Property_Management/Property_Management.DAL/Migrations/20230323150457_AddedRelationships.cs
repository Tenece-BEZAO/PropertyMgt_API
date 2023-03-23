using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    public partial class AddedRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TenantDetails_PropertyId",
                table: "TenantDetails");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_LoggedBy",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_LeaseDetails_PropertyId",
                table: "LeaseDetails");

            migrationBuilder.DropColumn(
                name: "ReportedToStaffId",
                table: "MaintenanceRequests");

            migrationBuilder.RenameIndex(
                name: "IX_TenantId",
                table: "TenantDetails",
                newName: "IX_TenantId1");

            migrationBuilder.RenameIndex(
                name: "IX_Email",
                table: "TenantDetails",
                newName: "IX_Email1");

            migrationBuilder.AddColumn<byte[]>(
                name: "Concurrency",
                table: "TenantDetails",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Concurrency",
                table: "Roles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Concurrency",
                table: "Propertys",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Managers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Concurrency",
                table: "Managers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InspectionCheckInspectionId",
                table: "Managers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Concurrency",
                table: "MaintenanceRequests",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Concurrency",
                table: "leasePayments",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Property_Number",
                table: "LeaseDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Concurrency",
                table: "LeaseDetails",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InspectionCheck",
                columns: table => new
                {
                    InspectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateLogged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InspectedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoOfUnits = table.Column<int>(type: "int", nullable: false),
                    UnitIdNumber = table.Column<int>(type: "int", nullable: false),
                    NoOfDevicesDamaged = table.Column<int>(type: "int", nullable: false),
                    Concurrency = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionCheck", x => x.InspectionId);
                    table.ForeignKey(
                        name: "FK_InspectionCheck_Managers_InspectedBy",
                        column: x => x.InspectedBy,
                        principalTable: "Managers",
                        principalColumn: "ManagerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantDetails_PropertyId",
                table: "TenantDetails",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Email",
                table: "Managers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_InspectionCheckInspectionId",
                table: "Managers",
                column: "InspectionCheckInspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_LoggedBy",
                table: "MaintenanceRequests",
                column: "LoggedBy",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyId",
                table: "LeaseDetails",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Property_Number",
                table: "LeaseDetails",
                column: "Tenant_Property_Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantId",
                table: "LeaseDetails",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InspectionCheck_InspectedBy",
                table: "InspectionCheck",
                column: "InspectedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UnitIdNumber",
                table: "InspectionCheck",
                column: "UnitIdNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_InspectionCheck_InspectionCheckInspectionId",
                table: "Managers",
                column: "InspectionCheckInspectionId",
                principalTable: "InspectionCheck",
                principalColumn: "InspectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_InspectionCheck_InspectionCheckInspectionId",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "InspectionCheck");

            migrationBuilder.DropIndex(
                name: "IX_TenantDetails_PropertyId",
                table: "TenantDetails");

            migrationBuilder.DropIndex(
                name: "IX_Email",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_InspectionCheckInspectionId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_LoggedBy",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_PropertyId",
                table: "LeaseDetails");

            migrationBuilder.DropIndex(
                name: "IX_Tenant_Property_Number",
                table: "LeaseDetails");

            migrationBuilder.DropIndex(
                name: "IX_TenantId",
                table: "LeaseDetails");

            migrationBuilder.DropColumn(
                name: "Concurrency",
                table: "TenantDetails");

            migrationBuilder.DropColumn(
                name: "Concurrency",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Concurrency",
                table: "Propertys");

            migrationBuilder.DropColumn(
                name: "Concurrency",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "InspectionCheckInspectionId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Concurrency",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "Concurrency",
                table: "leasePayments");

            migrationBuilder.DropColumn(
                name: "Concurrency",
                table: "LeaseDetails");

            migrationBuilder.RenameIndex(
                name: "IX_TenantId1",
                table: "TenantDetails",
                newName: "IX_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Email1",
                table: "TenantDetails",
                newName: "IX_Email");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ReportedToStaffId",
                table: "MaintenanceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Tenant_Property_Number",
                table: "LeaseDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_TenantDetails_PropertyId",
                table: "TenantDetails",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_LoggedBy",
                table: "MaintenanceRequests",
                column: "LoggedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseDetails_PropertyId",
                table: "LeaseDetails",
                column: "PropertyId");
        }
    }
}
