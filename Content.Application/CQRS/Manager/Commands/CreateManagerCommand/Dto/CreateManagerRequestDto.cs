namespace Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;

public class CreateManagerRequestDto
{
	public required string Name { get; init; }
	public required string Surname { get; init; }
	public required string Password { get; init; }
}
