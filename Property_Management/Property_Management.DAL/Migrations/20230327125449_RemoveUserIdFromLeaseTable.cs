using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserIdFromLeaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Leases_LandLordOrManagerId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_Leases_TenantId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_UserId",
                table: "Leases");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Leases");

            migrationBuilder.CreateIndex(
                name: "IX_LandLordOrManagerId",
                table: "Leases",
                column: "LandLordOrManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantId",
                table: "Leases",
                column: "TenantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LandLordOrManagerId",
                table: "Leases");

            migrationBuilder.DropIndex(
                name: "IX_TenantId",
                table: "Leases");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Leases",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_LandLordOrManagerId",
                table: "Leases",
                column: "LandLordOrManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Leases_TenantId",
                table: "Leases",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserId",
                table: "Leases",
                column: "UserId",
                unique: true);
        }
    }
}
