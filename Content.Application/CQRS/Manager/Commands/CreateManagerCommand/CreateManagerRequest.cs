using Demen.Content.Application.Manager.Base;
using Demen.Content.Application.Manager.Commands.CreateManagerCommand.Dto;
using MediatR;

namespace Demen.Content.Application.Manager.Commands.CreateManagerCommand;

public class CreateManagerRequest
	: IBaseCommandRequest<CreateManagerResponse, CreateManagerRequestDto>
{
	public CreateManagerRequestDto RequestDto { get; init; }

	public CreateManagerRequest(CreateManagerRequestDto requestDto)
	{
		RequestDto = requestDto;
	}
}
