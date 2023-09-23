using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demen.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class AddedDefaultValuesToEmailEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_email_type",
                table: "emails");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "emails",
                type: "varchar",
                nullable: false,
                defaultValue: "Personal",
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "emails",
                type: "varchar",
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AddCheckConstraint(
                name: "CK_email_status",
                table: "emails",
                sql: "status IN ('Active', 'Inactive', 'Deleted')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_email_type",
                table: "emails",
                sql: "type IN ('Personal', 'Corporate')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_email_status",
                table: "emails");

            migrationBuilder.DropCheckConstraint(
                name: "CK_email_type",
                table: "emails");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "emails",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldDefaultValue: "Personal");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "emails",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldDefaultValue: "Active");

            migrationBuilder.AddCheckConstraint(
                name: "CK_email_type",
                table: "emails",
                sql: "[type] IN ('Personal', 'Corporate')");
        }
    }
}
