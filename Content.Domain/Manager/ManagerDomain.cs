using Demen.Common.Enums;
using Demen.Content.Domain.Base;
using Demen.Content.Domain.Email;

namespace Demen.Content.Domain.Manager;

public class ManagerDomain : BaseDomain
{
	// ---- properties ------------------------------------------------------ //
	public string Name { get; init; }
	public string Surname { get; init; }
	public string Password { get; init; }

	// ---- relationships --------------------------------------------------- //

	public ICollection<EmailDomain>? Emails { get; init; }

	// ---- constructors ---------------------------------------------------- //
	public ManagerDomain(
		int id,
		Guid externalId,
		Status status,
		DateTime createdAt,
		DateTime? updatedAt,
		DateTime? deletedAt,
		string name,
		string surname,
		string password,
		ICollection<EmailDomain>? emails
	)
	{
		Id = id;
		ExternalId = externalId;
		Status = status;
		CreatedAt = createdAt;
		UpdatedAt = updatedAt;
		DeletedAt = deletedAt;
		Name = name;
		Surname = surname;
		Password = password;
		Emails = emails;
	}

	// ---- factories ------------------------------------------------------- //
	public static ManagerDomain Create(
		string name,
		string surname,
		string password,
		string? email = null,
		EmailType? emailType = null
	)
	{
		var emails = email is not null
			? new List<EmailDomain>()
			{
				EmailDomain.Create(
					managerId: 0,
					address: email,
					type: emailType
				)
			}
			: null;

		return new ManagerDomain(
			id: 0,
			externalId: Guid.Empty,
			status: Status.Active,
			createdAt: DateTime.UtcNow,
			updatedAt: null,
			deletedAt: null,
			name: name,
			surname: surname,
			password: password,
			emails: emails
		);
	}
}
