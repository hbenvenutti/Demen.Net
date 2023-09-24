using Demen.Common.Enums;
using Demen.Domain.Management.Email;

namespace Demen.Data.Entities;

public class EmailEntity : BaseEntity
{
	// ---- properties ------------------------------------------------------ //

	public required string Address { get; set; }
	public required bool IsVerified { get; set; }
	public required EmailType Type { get; set; }

	// ---- relationships --------------------------------------------------- //

	public required int ManagerId { get; set; }
	public required ManagerEntity? Manager { get; set; }

	// ---- operators ------------------------------------------------------- //

	public static implicit operator EmailEntity(EmailDomain emailDomain) =>
		new ()
		{
			Id = emailDomain.Id,
			ExternalId = emailDomain.ExternalId,
			Address = emailDomain.Address,
			IsVerified = emailDomain.IsVerified,
			Status = emailDomain.Status,
			Type = emailDomain.Type,
			ManagerId = emailDomain.ManagerId,
			Manager = null,
			CreatedAt = emailDomain.CreatedAt,
			UpdatedAt = emailDomain.UpdatedAt,
			DeletedAt = emailDomain.DeletedAt
		};

	public static implicit operator EmailDomain?(EmailEntity? emailEntity)
	{
		if (emailEntity is null)
			return null;

		return new EmailDomain()
		{
			Id = emailEntity.Id,
			ExternalId = emailEntity.ExternalId,
			ManagerId = emailEntity.ManagerId,
			Manager = emailEntity.Manager,
			Address = emailEntity.Address,
			IsVerified = emailEntity.IsVerified,
			Status = emailEntity.Status,
			CreatedAt = emailEntity.CreatedAt,
			UpdatedAt = emailEntity.UpdatedAt,
			DeletedAt = emailEntity.DeletedAt,
			Type = emailEntity.Type
		};
	}
}
