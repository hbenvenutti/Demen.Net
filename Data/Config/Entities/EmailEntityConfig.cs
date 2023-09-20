using System.Diagnostics.CodeAnalysis;
using Demen.Common.Enums;
using Demen.Common.Helpers;
using Demen.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demen.Data.Config.Entities;

[ExcludeFromCodeCoverage]
public class EmailEntityConfig : IEntityTypeConfiguration<EmailEntity>
{
	private readonly string _personal = EmailType.Personal.ToString();
	private readonly string _corporate = EmailType.Corporate.ToString();
	private readonly string _active = Status.Active.ToString();
	private readonly string _inactive = Status.Inactive.ToString();
	private readonly string _deleted = Status.Deleted.ToString();

	public void Configure(EntityTypeBuilder<EmailEntity> builder)
	{

		builder
			.ToTable(
				name: "emails",
				buildAction: table =>
				{
					table
						.HasCheckConstraint(
							name: "CK_email_type",
							sql: $"type IN ('{_personal}', '{_corporate}')"
						);

					table
						.HasCheckConstraint(
							name: "CK_email_status",
							sql: $"status IN ('{_active}', '{_inactive}', '{_deleted}')"
						);
				});

		// ---- keys -------------------------------------------------------- //

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

		// ---- columns ----------------------------------------------------- //

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
			.HasDefaultValue(Status.Active)
			.IsRequired();

		builder
			.Property(email => email.Type)
			.HasColumnName("type")
			.HasConversion(
				emailType => emailType.ToString(),
				str => str.StringToEnum<EmailType>()
			)
			.HasColumnType("varchar")
			.HasDefaultValue(EmailType.Personal)
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

		// ---- indexes ----------------------------------------------------- //

		builder
			.HasIndex(email => email.Address)
			.IsUnique();
	}
}
