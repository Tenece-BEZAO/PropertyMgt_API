using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerId);
                });

            migrationBuilder.CreateTable(
                name: "Propertys",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    propertyTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    managerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propertys", x => x.PropertyId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "LeaseDetails",
                columns: table => new
                {
                    LeaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tenant_Property_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tenant_Unit_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Upcoming_Tenant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lease_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lease_Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lease_End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monthly_Rent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Security_Deposit_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Lease_Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaseDetails", x => new { x.LeaseId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_LeaseDetails_Propertys_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Propertys",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantDetails",
                columns: table => new
                {
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantDetails", x => x.TenantId);
                    table.ForeignKey(
                        name: "FK_TenantDetails_Propertys_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Propertys",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leasePayments",
                columns: table => new
                {
                    LeasePaymentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LateFees = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaidBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LeaseDetailsLeaseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LeaseDetailsTenantId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leasePayments", x => x.LeasePaymentId);
                    table.ForeignKey(
                        name: "FK_leasePayments_LeaseDetails_LeaseDetailsLeaseId_LeaseDetailsTenantId",
                        columns: x => new { x.LeaseDetailsLeaseId, x.LeaseDetailsTenantId },
                        principalTable: "LeaseDetails",
                        principalColumns: new[] { "LeaseId", "TenantId" });
                    table.ForeignKey(
                        name: "FK_leasePayments_TenantDetails_PaidBy",
                        column: x => x.PaidBy,
                        principalTable: "TenantDetails",
                        principalColumn: "TenantId");
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceRequests",
                columns: table => new
                {
                    RequestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoggedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportedToStaffId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MRNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateLogged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_MaintenanceRequests_TenantDetails_LoggedBy",
                        column: x => x.LoggedBy,
                        principalTable: "TenantDetails",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaseDetails_PropertyId",
                table: "LeaseDetails",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaseId",
                table: "leasePayments",
                column: "LeaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeasePaymentId",
                table: "leasePayments",
                column: "LeaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_leasePayments_LeaseDetailsLeaseId_LeaseDetailsTenantId",
                table: "leasePayments",
                columns: new[] { "LeaseDetailsLeaseId", "LeaseDetailsTenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_leasePayments_PaidBy",
                table: "leasePayments",
                column: "PaidBy");

            migrationBuilder.CreateIndex(
                name: "IX_PaidBy",
                table: "leasePayments",
                column: "LeaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_LoggedBy",
                table: "MaintenanceRequests",
                column: "LoggedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Email",
                table: "TenantDetails",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantDetails_PropertyId",
                table: "TenantDetails",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantId",
                table: "TenantDetails",
                column: "TenantId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "leasePayments");

            migrationBuilder.DropTable(
                name: "MaintenanceRequests");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "LeaseDetails");

            migrationBuilder.DropTable(
                name: "TenantDetails");

            migrationBuilder.DropTable(
                name: "Propertys");
        }
    }
}
