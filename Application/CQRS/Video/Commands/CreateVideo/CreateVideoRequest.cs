using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Video.Commands.CreateVideo.Dto;

namespace Demen.Application.CQRS.Video.Commands.CreateVideo;

public class CreateVideoRequest
	: IBaseCommandRequest<CreateVideoResponse, CreateVideoRequestDto>
{
	public required CreateVideoRequestDto RequestDto { get; init; }
}
