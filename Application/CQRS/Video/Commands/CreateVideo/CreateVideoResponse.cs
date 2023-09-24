using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Video.Commands.CreateVideo.Dto;
using Demen.Application.Dto;

namespace Demen.Application.CQRS.Video.Commands.CreateVideo;

public class CreateVideoResponse
	: IBaseCommandResponse<CreateVideoResponseDto>
{
	public required ResponseDto<CreateVideoResponseDto>
		ResponseDto { get; init; }
}
