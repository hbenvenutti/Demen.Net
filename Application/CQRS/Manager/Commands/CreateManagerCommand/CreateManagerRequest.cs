using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using MediatR;

namespace Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;

public class CreateManagerRequest
	: IBaseCommandRequest<CreateManagerResponse, CreateManagerRequestDto>, IRequest<CreateManagerResponse>
{
	public CreateManagerRequestDto RequestDto { get; init; }

	public CreateManagerRequest(CreateManagerRequestDto requestDto)
	{
		RequestDto = requestDto;
	}
}
