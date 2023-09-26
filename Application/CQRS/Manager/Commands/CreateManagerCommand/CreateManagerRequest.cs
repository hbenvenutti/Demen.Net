using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Application.Dto;
using Demen.Domain.Management.Manager;
using MediatR;

namespace Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;

public class CreateManagerRequest
	: IRequest<Response<CreateManagerResponseDto>>
{
	public required string Name { get; init; }
	public required string Surname { get; init; }
	public required string Password { get; init; }
	public required string Email { get; init; }
	public string? EmailType { get; init; }

	// ---- operators ------------------------------------------------------- //
	public static implicit operator ManagerDomain(CreateManagerRequest dto)
	{
		return ManagerDomain.Create(
			name: dto.Name,
			surname: dto.Surname,
			password: dto.Password
		);
	}
}
