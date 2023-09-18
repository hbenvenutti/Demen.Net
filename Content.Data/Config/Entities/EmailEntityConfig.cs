using System.Diagnostics.CodeAnalysis;
using Demen.Content.Common.Enums;
using Demen.Content.Common.Helpers;
using Demen.Content.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demen.Content.Data.Config.Entities;

[ExcludeFromCodeCoverage]
public class EmailEntityConfig : IEntityTypeConfiguration<EmailEntity>
{
	public void Configure(EntityTypeBuilder<EmailEntity> builder)
	{
		builder
			.ToTable("emails");

		builder
			.HasKey(email => email.Id)
			.HasName("pk_email_id");

		builder
			.HasOne(email => email.Manager)
			.WithMany(manager => manager.Emails)
			.HasForeignKey(email => email.ManagerId)
			.HasConstraintName("fk_manager_id");

		builder
			.Property(email => email.Id)
			.HasColumnName("id")
			.IsRequired();

		builder
			.Property(email => email.ManagerId)
			.HasColumnName("manager_id")
			.IsRequired();

		builder
			.Property(email => email.ExternalId)
			.HasColumnName("external_id")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(email => email.Status)
			.HasColumnName("status")
			.HasColumnType("varchar")
			.HasConversion(
				status => status.ToString(),
				str => str.StringToEnum<Status>()
			)
			.IsRequired();

		builder
			.Property(email => email.Type)
			.HasColumnName("type")
			.HasConversion(
				emailType => emailType.ToString(),
				str => str.StringToEnum<EmailType>()
			)
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(email => email.Address)
			.HasColumnName("address")
			.HasColumnType("varchar")
			.IsRequired();

		builder
			.Property(email => email.IsVerified)
			.HasColumnName("is_verified")
			.IsRequired();

		builder
			.Property(email => email.CreatedAt)
			.HasColumnName("created_at")
			.IsRequired();

		builder
			.Property(email => email.UpdatedAt)
			.HasColumnName("updated_at")
			.IsRequired(false);

		builder
			.Property(email => email.DeletedAt)
			.HasColumnName("deleted_at")
			.IsRequired(false);
	}
}
