using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Demen.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class CreateChannels : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<int>(
			name: "channel_id",
			table: "videos",
			type: "integer",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.CreateTable(
			name: "channels",
			columns: table => new
			{
				id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				youtube_id = table.Column<string>(type: "varchar", nullable: false),
				name = table.Column<string>(type: "varchar", nullable: false),
				thumbnail_url = table.Column<string>(type: "varchar", nullable: false),
				description = table.Column<string>(type: "varchar", nullable: true),
				external_id = table.Column<string>(type: "varchar", nullable: false),
				created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
				updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
				deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
				status = table.Column<string>(type: "varchar", nullable: false, defaultValue: "Active")
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_channel_id", x => x.id);
				table.CheckConstraint("CK_channel_status", "status IN ('Active', 'Inactive', 'Deleted')");
			});

		migrationBuilder.CreateIndex(
			name: "IX_videos_channel_id",
			table: "videos",
			column: "channel_id");

		migrationBuilder.AddForeignKey(
			name: "FK_video_channel_id",
			table: "videos",
			column: "channel_id",
			principalTable: "channels",
			principalColumn: "id",
			onDelete: ReferentialAction.Restrict);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "FK_video_channel_id",
			table: "videos");

		migrationBuilder.DropTable(
			name: "channels");

		migrationBuilder.DropIndex(
			name: "IX_videos_channel_id",
			table: "videos");

		migrationBuilder.DropColumn(
			name: "channel_id",
			table: "videos");
	}
}
