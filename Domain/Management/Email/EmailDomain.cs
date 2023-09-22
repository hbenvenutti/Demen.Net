using Demen.Common.Enums;
using Demen.Domain.Base;
using Demen.Domain.Management.Manager;

namespace Demen.Domain.Management.Email;

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
		Guid externalId,
		int managerId,
		string address,
		DateTime createdAt,
		int id = 0,
		Status status = Status.Active,
		ManagerDomain? manager = null,
		bool isVerified = false,
		DateTime? updatedAt = null,
		DateTime? deletedAt = null,
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
			externalId: Guid.Empty,
			managerId: managerId,
			address: address,
			createdAt: DateTime.Now,
			type: type ?? EmailType.Personal
		);
	}
}
