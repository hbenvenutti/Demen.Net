using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Video.Commands.CreateVideo.Dto;
using MediatR;

namespace Demen.Application.CQRS.Video.Commands.CreateVideo;

public class CreateVideoRequest : IRequest<Response<CreateVideoResponseDto>>
{
	public required string YoutubeId { get; init; }
}
