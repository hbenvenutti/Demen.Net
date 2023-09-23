using Demen.Application.CQRS.Base;
using Demen.Application.Dto;

namespace Demen.Application.CQRS.Manager.Commands.DeleteManager;

public class DeleteManagerResponse : IBaseCommandResponse<EmptyDto>
{
	public ResponseDto<EmptyDto> ResponseDto { get; init; }

	public DeleteManagerResponse(ResponseDto<EmptyDto> responseDto)
	{
		ResponseDto = responseDto;
	}
}
