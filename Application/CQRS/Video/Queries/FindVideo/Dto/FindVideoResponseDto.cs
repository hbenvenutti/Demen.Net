using Demen.Domain.Content.Video;

namespace Demen.Application.CQRS.Video.Queries.FindVideo.Dto;

public class FindVideoResponseDto
{
	public VideoDomain Video { get; init; }
}
