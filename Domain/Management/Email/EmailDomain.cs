using Demen.Common.Enums;
using Demen.Domain.Base;
using Demen.Domain.Management.Manager;

namespace Demen.Domain.Management.Email;

public class EmailDomain : BaseDomain
{
	// ---- properties ------------------------------------------------------ //

	public required string Address { get; init; }
	public required bool IsVerified { get; init; } = false;
	public required EmailType Type { get; init; }

	// ---- relationships --------------------------------------------------- //

	public required int ManagerId { get; init; }
	public required ManagerDomain? Manager { get; init; }

	// ---- factories ------------------------------------------------------- //

	public static EmailDomain Create(
		int managerId,
		string address,
		EmailType? type = null
	) =>
		new()
		{
			Id = 0,
			ExternalId = Guid.Empty,
			ManagerId = managerId,
			Address = address,
			CreatedAt = DateTime.Now,
			Type = type ?? EmailType.Personal,
			Status = Status.Active,
			Manager = null,
			UpdatedAt = null,
			DeletedAt = null,
			IsVerified = false,
		};
}
