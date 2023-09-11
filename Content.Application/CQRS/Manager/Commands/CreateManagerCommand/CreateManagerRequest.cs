using MediatR;
using Demen.Content.Application.CQRS.Base;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Content.Application.Manager.Commands.CreateManagerCommand;

namespace Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand;

public class CreateManagerRequest
	: IBaseCommandRequest<CreateManagerResponse, CreateManagerRequestDto>, IRequest<CreateManagerResponse>
{
	public CreateManagerRequestDto RequestDto { get; init; }

	public CreateManagerRequest(CreateManagerRequestDto requestDto)
	{
		RequestDto = requestDto;
	}
}