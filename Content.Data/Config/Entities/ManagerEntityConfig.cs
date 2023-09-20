using System.Diagnostics.CodeAnalysis;
using Demen.Common.Enums;
using Demen.Common.Helpers;
using Demen.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demen.Data.Config.Entities;

[ExcludeFromCodeCoverage]
public class ManagerEntityConfig : IEntityTypeConfiguration<ManagerEntity>
{
	public void Configure(EntityTypeBuilder<ManagerEntity> builder)
	{
		builder
			.ToTable("managers");

		builder
			.HasKey(manager => manager.Id)
			.HasName("pk_manager_id");

		builder
			.Property(manager => manager.Id)
			.HasColumnName("id")
			.IsRequired();

		builder
			.Property(manager => manager.ExternalId)
			.HasColumnName("external_id")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(manager => manager.Status)
			.HasColumnType("varchar")
			.HasColumnName("status")
			.HasConversion(
				status => status.ToString(),
				str => str.StringToEnum<Status>()
			)
			.IsRequired();

		builder
			.Property(manager => manager.Name)
			.HasColumnName("name")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(manager => manager.Surname)
			.HasColumnName("surname")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(manager => manager.Password)
			.HasColumnName("password")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(manager => manager.CreatedAt)
			.HasColumnName("created_at")
			.IsRequired();

		builder
			.Property(manager => manager.UpdatedAt)
			.HasColumnName("updated_at")
			.IsRequired(false);

		builder
			.Property(manager => manager.DeletedAt)
			.HasColumnName("deleted_at")
			.IsRequired(false);
	}
}
