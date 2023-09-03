using Demen.Content.Domain.Manager;

namespace Demen.Content.Data.Entities;

public class ManagerEntity : BaseEntity
{
	// ---- properties ------------------------------------------------------ //
	public required string Name { get; set; }
	public required string Surname { get; set; }
	public required string Password { get; set; }

	// ---- factories ------------------------------------------------------- //
	public static implicit operator ManagerDomain?(ManagerEntity? managerEntity)
	{
		if (managerEntity is null) return null;

		return new ManagerDomain(
			id: managerEntity.Id,
			externalId: managerEntity.ExternalId,
			status: managerEntity.Status,
			createdAt: managerEntity.CreatedAt,
			updatedAt: managerEntity.UpdatedAt,
			deletedAt: managerEntity.DeletedAt,
			name: managerEntity.Name,
			surname: managerEntity.Surname,
			password: managerEntity.Password
		);
	}

	public static implicit operator ManagerEntity(ManagerDomain managerDomain)
	{
		return new ManagerEntity
		{
			Id = managerDomain.Id,
			ExternalId = managerDomain.ExternalId,
			Status = managerDomain.Status,
			CreatedAt = managerDomain.CreatedAt,
			UpdatedAt = managerDomain.UpdatedAt,
			DeletedAt = managerDomain.DeletedAt,
			Name = managerDomain.Name,
			Surname = managerDomain.Surname,
			Password = managerDomain.Password
		};
	}
}
