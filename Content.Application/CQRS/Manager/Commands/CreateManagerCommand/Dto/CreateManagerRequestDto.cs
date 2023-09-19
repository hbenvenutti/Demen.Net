using Demen.Content.Domain.Manager;

namespace Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;

public class CreateManagerRequestDto
{
	public required string Name { get; init; }
	public required string Surname { get; init; }
	public required string Password { get; init; }

	// ---- operators ------------------------------------------------------- //
	public static implicit operator ManagerDomain(CreateManagerRequestDto dto)
	{
		return ManagerDomain.Create(
			name: dto.Name,
			surname: dto.Surname,
			password: dto.Password
		);
	}
}
