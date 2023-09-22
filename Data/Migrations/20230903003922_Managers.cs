using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Demen.Content.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class Managers : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "managers",
			columns: table => new
			{
				id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				name = table.Column<string>(type: "varchar", nullable: false),
				surname = table.Column<string>(type: "varchar", nullable: false),
				password = table.Column<string>(type: "varchar", nullable: false),
				external_id = table.Column<string>(type: "varchar", nullable: false),
				created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
				updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
				deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
				status = table.Column<string>(type: "varchar", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("id", x => x.id);
			});
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "managers");
	}
}
