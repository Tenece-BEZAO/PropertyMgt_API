using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Property_Management.DAL.Migrations
{
    /// <inheritdoc />
    public partial class droedOwnby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_LandLord_OwnedBy",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "OwnedBy",
                table: "Properties",
                newName: "LandLordId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_OwnedBy",
                table: "Properties",
                newName: "IX_Properties_LandLordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_LandLord_LandLordId",
                table: "Properties",
                column: "LandLordId",
                principalTable: "LandLord",
                principalColumn: "LandLordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_LandLord_LandLordId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "LandLordId",
                table: "Properties",
                newName: "OwnedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_LandLordId",
                table: "Properties",
                newName: "IX_Properties_OwnedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_LandLord_OwnedBy",
                table: "Properties",
                column: "OwnedBy",
                principalTable: "LandLord",
                principalColumn: "LandLordId");
        }
    }
}
