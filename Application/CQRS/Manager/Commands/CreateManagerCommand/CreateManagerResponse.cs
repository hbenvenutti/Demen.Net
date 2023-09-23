using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Application.Dto;

namespace Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;

public class CreateManagerResponse
	: IBaseCommandResponse<CreateManagerResponseDto>
{
	public ResponseDto<CreateManagerResponseDto> ResponseDto { get; init; }

	public CreateManagerResponse(ResponseDto<CreateManagerResponseDto> responseDto)
	{
		ResponseDto = responseDto;
	}
}
