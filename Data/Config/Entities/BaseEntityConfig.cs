using Demen.Common.Enums;
using Demen.Common.Helpers;
using Demen.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demen.Data.Config;

public static class  BaseEntityConfig
{
	private static readonly string Active = Status.Active.ToString();
	private static readonly string Inactive = Status.Inactive.ToString();
	private static readonly string Deleted = Status.Deleted.ToString();

	private static readonly string StatusConstraintSql =
		$"status IN ('{Active}', '{Inactive}', '{Deleted}')";

	// ---- methods --------------------------------------------------------- //

	public static void CreateStatusConstraint<T>(
		this TableBuilder<T> table,
		string name
	) where T : BaseEntity => table
		.HasCheckConstraint(
			name: name,
			sql: StatusConstraintSql
		);

	public static void ConfigureBaseEntityProperties<T>(
		this EntityTypeBuilder<T> builder
	) where T : BaseEntity
	{
		builder
			.Property(entity => entity.Id)
			.HasColumnName("id")
			.IsRequired();

		builder
			.Property(entity => entity.ExternalId)
			.HasColumnName("external_id")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(entity => entity.Status)
			.HasColumnType("varchar")
			.HasColumnName("status")
			.HasConversion(
				status => status.ToString(),
				str => str.StringToEnum<Status>()
			)
			.HasDefaultValue(Status.Active)
			.IsRequired();

		builder
			.Property(entity => entity.CreatedAt)
			.HasColumnName("created_at")
			.IsRequired();

		builder
			.Property(entity => entity.UpdatedAt)
			.HasColumnName("updated_at")
			.IsRequired(false);

		builder
			.Property(entity => entity.DeletedAt)
			.HasColumnName("deleted_at")
			.IsRequired(false);
	}
}
