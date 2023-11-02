using Demen.Common.Enums;
using Demen.Domain.Management.Email;
using Demen.Domain.Management.Manager;

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

	public static implicit operator EmailDomain(EmailEntity emailEntity) =>
		new ()
		{
			Id = emailEntity.Id,
			ExternalId = emailEntity.ExternalId,
			ManagerId = emailEntity.ManagerId,
			Address = emailEntity.Address,
			IsVerified = emailEntity.IsVerified,
			Status = emailEntity.Status,
			CreatedAt = emailEntity.CreatedAt,
			UpdatedAt = emailEntity.UpdatedAt,
			DeletedAt = emailEntity.DeletedAt,
			Type = emailEntity.Type,

			Manager = emailEntity.Manager is null
				? null
				: (ManagerDomain) emailEntity.Manager,
		};
}
