using Demen.Domain.Management.Manager;

namespace Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;

public class GetManagerResponseDto
{
	// ---- properties ------------------------------------------------------ //
	public Guid Id { get; init; }
	public required string Name { get; init; }
	public required string Surname { get; init; }
	public required string Status { get; init; }
	public DateTime CreatedAt { get; init; }
	public DateTime? UpdatedAt { get; init; }
	public DateTime? DeletedAt { get; init; }

	// ---- factories ------------------------------------------------------- //
	public static implicit operator GetManagerResponseDto(
		ManagerDomain managerDomain
	)
	{
		return new GetManagerResponseDto()
		{
			Id = managerDomain.ExternalId,
			Name = managerDomain.Name,
			Surname = managerDomain.Surname,
			Status = managerDomain.Status.ToString(),
			CreatedAt = managerDomain.CreatedAt,
			UpdatedAt = managerDomain.UpdatedAt,
			DeletedAt = managerDomain.DeletedAt
		};
	}
}
