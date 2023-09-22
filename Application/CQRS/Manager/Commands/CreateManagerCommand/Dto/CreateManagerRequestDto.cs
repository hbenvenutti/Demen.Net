using Demen.Domain.Management.Manager;

namespace Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;

public class CreateManagerRequestDto
{
	public required string Name { get; init; }
	public required string Surname { get; init; }
	public required string Password { get; init; }
	public required string Email { get; init; }
	public string? EmailType { get; init; }

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
