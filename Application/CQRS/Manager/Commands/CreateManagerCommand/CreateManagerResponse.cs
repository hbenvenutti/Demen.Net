using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Ether.Outcomes;

namespace Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;

public class CreateManagerResponse
	: IBaseCommandResponse<CreateManagerResponseDto>
{
	public IOutcome<CreateManagerResponseDto> Outcome { get; init; }

	public CreateManagerResponse(IOutcome<CreateManagerResponseDto> outcome)
	{
		Outcome = outcome;
	}
}
