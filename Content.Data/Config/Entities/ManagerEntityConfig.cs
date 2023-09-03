using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Demen.Content.Data.Entities;

namespace Demen.Content.Data.Config.Entities;

public class ManagerEntityConfig : IEntityTypeConfiguration<ManagerEntity>
{
	public void Configure(EntityTypeBuilder<ManagerEntity> builder)
	{
		builder
			.ToTable("managers");

		builder
			.HasKey(manager => manager.Id)
			.HasName("id");

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
			.Property(manager => manager.StatusString)
			.HasColumnType("varchar")
			.HasColumnName("status")
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

		builder
			.Ignore(manager => manager.Status);
	}
}
