using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demen.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusConstraintToManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "managers",
                type: "varchar",
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AddCheckConstraint(
                name: "CK_manager_status",
                table: "managers",
                sql: "status IN ('Active', 'Inactive', 'Deleted')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_manager_status",
                table: "managers");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "managers",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldDefaultValue: "Active");
        }
    }
}
