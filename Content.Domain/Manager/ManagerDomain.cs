using Demen.Content.Common.Enums;
using Demen.Content.Domain.Base;

namespace Demen.Content.Domain.Manager;

public class ManagerDomain : BaseDomain
{
	// ---- properties ------------------------------------------------------ //
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Password { get; set; }

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
		string password
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
	}

	// ---- factories ------------------------------------------------------- //
	public ManagerDomain Create(
		string name,
		string surname,
		string password
	)
	{
		return new ManagerDomain(
			id: 0,
			externalId: Guid.Empty,
			status: Status.Active,
			createdAt: DateTime.UtcNow,
			updatedAt: null,
			deletedAt: null,
			name: name,
			surname: surname,
			password: password
		);
	}
}
