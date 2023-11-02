using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demen.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class AddedUniquenessToYoutubeIds : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateIndex(
			name: "IX_video_youtube_id",
			table: "videos",
			column: "youtube_id",
			unique: true);

		migrationBuilder.CreateIndex(
			name: "IX_channel_youtube_id",
			table: "channels",
			column: "youtube_id",
			unique: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropIndex(
			name: "IX_video_youtube_id",
			table: "videos");

		migrationBuilder.DropIndex(
			name: "IX_channel_youtube_id",
			table: "channels");
	}
}
