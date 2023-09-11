using Ether.Outcomes;
using Demen.Content.Application.CQRS.Base;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;

namespace Demen.Content.Application.Manager.Commands.CreateManagerCommand;

public class CreateManagerResponse
	: IBaseCommandResponse<CreateManagerResponseDto>
{
	public IOutcome<CreateManagerResponseDto> Outcome { get; init; }

	public CreateManagerResponse(IOutcome<CreateManagerResponseDto> outcome)
	{
		Outcome = outcome;
	}
}