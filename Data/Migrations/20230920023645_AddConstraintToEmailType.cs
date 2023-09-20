using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demen.Content.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraintToEmailType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_email_type",
                table: "emails",
                sql: "type IN ('Personal', 'Corporate')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_email_type",
                table: "emails");
        }
    }
}
