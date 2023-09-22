using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Demen.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class CreateVideos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_manager_id",
                table: "emails");

            migrationBuilder.DropPrimaryKey(
                name: "pk_manager_id",
                table: "managers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_email_id",
                table: "emails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_manager_id",
                table: "managers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_email_id",
                table: "emails",
                column: "id");

            migrationBuilder.CreateTable(
                name: "videos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "varchar", nullable: false),
                    description = table.Column<string>(type: "varchar", nullable: false),
                    thumbnail_url = table.Column<string>(type: "varchar", nullable: false),
                    youtube_id = table.Column<string>(type: "varchar", nullable: false),
                    manager_id = table.Column<int>(type: "integer", nullable: false),
                    external_id = table.Column<string>(type: "varchar", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "varchar", nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_video_id", x => x.id);
                    table.CheckConstraint("CK_video_status", "status IN ('Active', 'Inactive', 'Deleted')");
                    table.ForeignKey(
                        name: "FK_video_manager_id",
                        column: x => x.manager_id,
                        principalTable: "managers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_videos_manager_id",
                table: "videos",
                column: "manager_id");

            migrationBuilder.AddForeignKey(
                name: "FK_email_manager_id",
                table: "emails",
                column: "manager_id",
                principalTable: "managers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_email_manager_id",
                table: "emails");

            migrationBuilder.DropTable(
                name: "videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_manager_id",
                table: "managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_email_id",
                table: "emails");

            migrationBuilder.AddPrimaryKey(
                name: "pk_manager_id",
                table: "managers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_email_id",
                table: "emails",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_manager_id",
                table: "emails",
                column: "manager_id",
                principalTable: "managers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
