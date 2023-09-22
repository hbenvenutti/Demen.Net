using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demen.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteredEmailFKDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_email_manager_id",
                table: "emails");

            migrationBuilder.AddForeignKey(
                name: "FK_email_manager_id",
                table: "emails",
                column: "manager_id",
                principalTable: "managers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_email_manager_id",
                table: "emails");

            migrationBuilder.AddForeignKey(
                name: "FK_email_manager_id",
                table: "emails",
                column: "manager_id",
                principalTable: "managers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
