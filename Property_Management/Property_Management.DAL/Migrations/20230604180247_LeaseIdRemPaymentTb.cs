using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LeaseIdRemPaymentTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Leases_LeaseId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_LeaseId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LeaseId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Leases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaseId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Leases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LeaseId",
                table: "Payments",
                column: "LeaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Leases_LeaseId",
                table: "Payments",
                column: "LeaseId",
                principalTable: "Leases",
                principalColumn: "Id");
        }
    }
}
