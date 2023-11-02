using Demen.Domain.Content.Video;

namespace Demen.Application.CQRS.Video.Commands.CreateVideo.Dto;

public class CreateVideoResponseDto
{
	public required Guid Id { get; init; }
	public required string YoutubeId { get; init; }
	public required Guid? ChannelId { get; init; }
	public required string Title { get; init; }
	public required string Description { get; init; }
	public required string ThumbnailUrl { get; init; }
	public required DateTime PublishedAt { get; init; }
	public required DateTime CreatedAt { get; init; }

	// ---------------------------------------------------------------------- //

	public static implicit operator CreateVideoResponseDto(VideoDomain domain)
		=> new()
		{
			Id = domain.ExternalId,
			YoutubeId = domain.YoutubeId,
			Title = domain.Title,
			Description = domain.Description,
			ThumbnailUrl = domain.ThumbnailUrl,
			PublishedAt = domain.PublishedAt,
			CreatedAt = domain.CreatedAt,
			ChannelId = domain.Channel?.ExternalId ?? null
		};
}
