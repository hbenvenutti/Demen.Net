using Demen.Common.Enums;
using Demen.Domain.Base;
using Demen.Domain.Content;
using Demen.Domain.Management.Email;

namespace Demen.Domain.Management.Manager;

public class ManagerDomain : BaseDomain
{
	// ---- properties ------------------------------------------------------ //
	public string Name { get; init; }
	public string Surname { get; init; }
	public string Password { get; init; }

	// ---- relationships --------------------------------------------------- //

	public ICollection<EmailDomain>? Emails { get; init; }
	public ICollection<VideoDomain>? Videos { get; init; }

	// ---- constructors ---------------------------------------------------- //

	public ManagerDomain(
		string name,
		string surname,
		string password,
		Guid externalId,
		DateTime createdAt,
		int id = 0,
		Status status = Status.Active,
		ICollection<EmailDomain>? emails = null,
		ICollection<VideoDomain>? videos = null,
		DateTime? updatedAt = null,
		DateTime? deletedAt = null
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
		Videos = videos;
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
			createdAt: DateTime.UtcNow,
			name: name,
			surname: surname,
			password: password,
			emails: emails
		);
	}
}
