using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demen.Content.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToEmailAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_emails_address",
                table: "emails",
                column: "address",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_emails_address",
                table: "emails");
        }
    }
}
