using Demen.Content.Common.Enums;
using Demen.Content.Domain.Base;
using Demen.Content.Domain.Manager;

namespace Demen.Content.Domain.Email;

public class EmailDomain : BaseDomain
{
	// ---- properties ------------------------------------------------------ //

	public string Address { get; init; }
	public bool IsVerified { get; init; }

	public EmailType Type { get; init; }

	// ---- relationships --------------------------------------------------- //

	public int ManagerId { get; init; }
	public ManagerDomain? Manager { get; init; }

	// ---- constructors ---------------------------------------------------- //

	public EmailDomain(
		int id,
		Guid externalId,
		Status status,
		int managerId,
		ManagerDomain? manager,
		string address,
		bool isVerified,
		DateTime createdAt,
		DateTime? updatedAt,
		DateTime? deletedAt,
		EmailType type = EmailType.Personal
	)
	{
		Id = id;
		ExternalId = externalId;
		Status = status;
		ManagerId = managerId;
		Manager = manager;
		Address = address;
		IsVerified = isVerified;
		Type = type;
		CreatedAt = createdAt;
		UpdatedAt = updatedAt;
		DeletedAt = deletedAt;
	}

	// ---- factories ------------------------------------------------------- //

	public static EmailDomain Create(
		int managerId,
		string address,
		EmailType? type = null
	)
	{
		return new EmailDomain(
			id: 0,
			externalId: Guid.Empty,
			managerId: managerId,
			manager: null,
			address: address,
			isVerified: false,
			status: Status.Active,
			createdAt: DateTime.Now,
			updatedAt: null,
			deletedAt: null,
			type: type ?? EmailType.Personal
		);
	}
}
