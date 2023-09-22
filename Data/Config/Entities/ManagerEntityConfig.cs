using System.Diagnostics.CodeAnalysis;
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
			.ToTable(name: "managers", buildAction: table =>
				table.CreateStatusConstraint(name: "CK_manager_status")
			);

		builder
			.ConfigureBaseEntityProperties();

		builder
			.HasKey(manager => manager.Id)
			.HasName("PK_manager_id");

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
	}
}
