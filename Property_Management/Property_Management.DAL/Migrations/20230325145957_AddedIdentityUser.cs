using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    public partial class AddedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leasePayments_LeaseDetails_LeaseDetailsLeaseId_LeaseDetailsTenantId",
                table: "leasePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_leasePayments_TenantDetails_PaidBy",
                table: "leasePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_TenantDetails_LoggedBy",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_InspectionCheck_InspectionCheckInspectionId",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "LeaseDetails");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TenantDetails");

            migrationBuilder.DropIndex(
                name: "IX_Managers_InspectionCheckInspectionId",
                table: "Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leasePayments",
                table: "leasePayments");

            migrationBuilder.DropColumn(
                name: "InspectionCheckInspectionId",
                table: "Managers");

            migrationBuilder.RenameTable(
                name: "leasePayments",
                newName: "LeasePayments");

            migrationBuilder.RenameIndex(
                name: "IX_leasePayments_PaidBy",
                table: "LeasePayments",
                newName: "IX_LeasePayments_PaidBy");

            migrationBuilder.RenameIndex(
                name: "IX_leasePayments_LeaseDetailsLeaseId_LeaseDetailsTenantId",
                table: "LeasePayments",
                newName: "IX_LeasePayments_LeaseDetailsLeaseId_LeaseDetailsTenantId");

            migrationBuilder.AlterColumn<string>(
                name: "LeaseDetailsTenantId",
                table: "LeasePayments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LeaseDetailsLeaseId",
                table: "LeasePayments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "InspectionCheck",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeasePayments",
                table: "LeasePayments",
                column: "LeasePaymentId");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Concurrency = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Propertys_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Propertys",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leases",
                columns: table => new
                {
                    LeaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tenant_Property_Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tenant_Unit_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Upcoming_Tenant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lease_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lease_Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lease_End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monthly_Rent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Security_Deposit_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Lease_Status = table.Column<bool>(type: "bit", nullable: false),
                    Concurrency = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leases", x => new { x.LeaseId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_Leases_Propertys_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Propertys",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Concurrency = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantId);
                    table.ForeignKey(
                        name: "FK_Tenants_Propertys_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Propertys",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InspectionCheck_ManagerId",
                table: "InspectionCheck",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PropertyId",
                table: "AspNetUsers",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyId",
                table: "Leases",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Property_Number",
                table: "Leases",
                column: "Tenant_Property_Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantId",
                table: "Leases",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Email1",
                table: "Tenants",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantId1",
                table: "Tenants",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PropertyId",
                table: "Tenants",
                column: "PropertyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionCheck_Managers_ManagerId",
                table: "InspectionCheck",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeasePayments_Leases_LeaseDetailsLeaseId_LeaseDetailsTenantId",
                table: "LeasePayments",
                columns: new[] { "LeaseDetailsLeaseId", "LeaseDetailsTenantId" },
                principalTable: "Leases",
                principalColumns: new[] { "LeaseId", "TenantId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeasePayments_Tenants_PaidBy",
                table: "LeasePayments",
                column: "PaidBy",
                principalTable: "Tenants",
                principalColumn: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Tenants_LoggedBy",
                table: "MaintenanceRequests",
                column: "LoggedBy",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionCheck_Managers_ManagerId",
                table: "InspectionCheck");

            migrationBuilder.DropForeignKey(
                name: "FK_LeasePayments_Leases_LeaseDetailsLeaseId_LeaseDetailsTenantId",
                table: "LeasePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_LeasePayments_Tenants_PaidBy",
                table: "LeasePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Tenants_LoggedBy",
                table: "MaintenanceRequests");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Leases");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeasePayments",
                table: "LeasePayments");

            migrationBuilder.DropIndex(
                name: "IX_InspectionCheck_ManagerId",
                table: "InspectionCheck");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "InspectionCheck");

            migrationBuilder.RenameTable(
                name: "LeasePayments",
                newName: "leasePayments");

            migrationBuilder.RenameIndex(
                name: "IX_LeasePayments_PaidBy",
                table: "leasePayments",
                newName: "IX_leasePayments_PaidBy");

            migrationBuilder.RenameIndex(
                name: "IX_LeasePayments_LeaseDetailsLeaseId_LeaseDetailsTenantId",
                table: "leasePayments",
                newName: "IX_leasePayments_LeaseDetailsLeaseId_LeaseDetailsTenantId");

            migrationBuilder.AddColumn<string>(
                name: "InspectionCheckInspectionId",
                table: "Managers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LeaseDetailsTenantId",
                table: "leasePayments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LeaseDetailsLeaseId",
                table: "leasePayments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leasePayments",
                table: "leasePayments",
                column: "LeasePaymentId");

            migrationBuilder.CreateTable(
                name: "LeaseDetails",
                columns: table => new
                {
                    LeaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Concurrency = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Lease_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lease_End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lease_Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lease_Status = table.Column<bool>(type: "bit", nullable: false),
                    Monthly_Rent = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Security_Deposit_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tenant_Property_Number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tenant_Unit_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Upcoming_Tenant = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Concurrency = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TenantDetails",
                columns: table => new
                {
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Concurrency = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_Managers_InspectionCheckInspectionId",
                table: "Managers",
                column: "InspectionCheckInspectionId");

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
                name: "IX_Email1",
                table: "TenantDetails",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantDetails_PropertyId",
                table: "TenantDetails",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantId1",
                table: "TenantDetails",
                column: "TenantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_leasePayments_LeaseDetails_LeaseDetailsLeaseId_LeaseDetailsTenantId",
                table: "leasePayments",
                columns: new[] { "LeaseDetailsLeaseId", "LeaseDetailsTenantId" },
                principalTable: "LeaseDetails",
                principalColumns: new[] { "LeaseId", "TenantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_leasePayments_TenantDetails_PaidBy",
                table: "leasePayments",
                column: "PaidBy",
                principalTable: "TenantDetails",
                principalColumn: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_TenantDetails_LoggedBy",
                table: "MaintenanceRequests",
                column: "LoggedBy",
                principalTable: "TenantDetails",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_InspectionCheck_InspectionCheckInspectionId",
                table: "Managers",
                column: "InspectionCheckInspectionId",
                principalTable: "InspectionCheck",
                principalColumn: "InspectionId");
        }
    }
}
