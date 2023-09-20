using Demen.Content.Domain.Manager;

namespace Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;

public class CreateManagerResponseDto
{
	// ---- properties ------------------------------------------------------ //
	public Guid Id { get; init; }
	public required string Name { get; init; }
	public required string Surname { get; init; }
	public DateTime CreatedAt { get; init; }

	// ---- operators ------------------------------------------------------- //
	public static implicit operator CreateManagerResponseDto(
		ManagerDomain managerDomain
	)
	{
		return new CreateManagerResponseDto()
		{
			Id = managerDomain.ExternalId,
			Name = managerDomain.Name,
			Surname = managerDomain.Surname,
			CreatedAt = managerDomain.CreatedAt
		};
	}
}
