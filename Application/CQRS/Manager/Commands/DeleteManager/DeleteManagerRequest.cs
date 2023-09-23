using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Commands.DeleteManager.Dto;

namespace Demen.Application.CQRS.Manager.Commands.DeleteManager;

public class DeleteManagerRequest
	: IBaseCommandRequest<DeleteManagerResponse, DeleteManagerRequestDto>
{
	public DeleteManagerRequestDto RequestDto { get; init; }

	public DeleteManagerRequest(DeleteManagerRequestDto requestDto)
	{
		RequestDto = requestDto;
	}
}
