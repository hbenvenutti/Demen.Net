using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Video.Queries.FindVideo.Dto;
using MediatR;

namespace Demen.Application.CQRS.Video.Queries.FindVideo;

public class FindVideoRequest : IRequest<Response<FindVideoResponseDto>>
{
	public required Guid Id { get; init; }
	public bool IncludeChannel { get; init; }
	public bool IncludeManager { get; init; }
}
