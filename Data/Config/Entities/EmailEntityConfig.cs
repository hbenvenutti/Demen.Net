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
						.CreateStatusConstraint(name: "CK_email_status");
				});

		// ---- keys -------------------------------------------------------- //

		builder
			.HasKey(email => email.Id)
			.HasName("PK_email_id");

		builder
			.ConfigureBaseEntityProperties();

		builder
			.HasOne(email => email.Manager)
			.WithMany(manager => manager.Emails)
			.HasForeignKey(email => email.ManagerId)
			.HasConstraintName("FK_email_manager_id");

		builder
			.Property(email => email.ManagerId)
			.HasColumnName("manager_id")
			.IsRequired();

		// ---- columns ----------------------------------------------------- //

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

		// ---- indexes ----------------------------------------------------- //

		builder
			.HasIndex(email => email.Address)
			.IsUnique();
	}
}
