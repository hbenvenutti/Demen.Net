using Demen.Common.Enums;
using Demen.Domain.Email;

namespace Demen.Data.Entities;

public class EmailEntity : BaseEntity
{
	// ---- properties ------------------------------------------------------ //

	public required string Address { get; set; }
	public bool IsVerified { get; set; }
	public required EmailType Type { get; set; }

	// ---- relationships --------------------------------------------------- //

	public int ManagerId { get; set; }
	public ManagerEntity? Manager { get; set; }

	// ---- operators ------------------------------------------------------- //

	public static implicit operator EmailEntity?(EmailDomain? emailDomain)
	{
		if (emailDomain is null)
			return null;

		return new EmailEntity()
		{
			Id = emailDomain.Id,
			ExternalId = emailDomain.ExternalId,
			Address = emailDomain.Address,
			IsVerified = emailDomain.IsVerified,
			Status = emailDomain.Status,
			Type = emailDomain.Type,
			ManagerId = emailDomain.ManagerId,
			Manager = emailDomain.Manager,
			CreatedAt = emailDomain.CreatedAt,
			UpdatedAt = emailDomain.UpdatedAt,
			DeletedAt = emailDomain.DeletedAt
		};
	}

	public static implicit operator EmailDomain?(EmailEntity? emailEntity)
	{
		if (emailEntity is null)
			return null;

		return new EmailDomain(
			id: emailEntity.Id,
			externalId: emailEntity.ExternalId,
			managerId: emailEntity.ManagerId,
			manager: emailEntity.Manager,
			address: emailEntity.Address,
			isVerified: emailEntity.IsVerified,
			status: emailEntity.Status,
			createdAt: emailEntity.CreatedAt,
			updatedAt: emailEntity.UpdatedAt,
			deletedAt: emailEntity.DeletedAt
		);
	}
}
