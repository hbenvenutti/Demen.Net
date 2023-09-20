using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Demen.Content.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "id",
                table: "managers");

            migrationBuilder.AddPrimaryKey(
                name: "pk_manager_id",
                table: "managers",
                column: "id");

            migrationBuilder.CreateTable(
                name: "emails",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    manager_id = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "varchar", nullable: false),
                    is_verified = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<string>(type: "varchar", nullable: false),
                    external_id = table.Column<string>(type: "varchar", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_manager_id",
                        column: x => x.manager_id,
                        principalTable: "managers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emails_manager_id",
                table: "emails",
                column: "manager_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emails");

            migrationBuilder.DropPrimaryKey(
                name: "pk_manager_id",
                table: "managers");

            migrationBuilder.AddPrimaryKey(
                name: "id",
                table: "managers",
                column: "id");
        }
    }
}
