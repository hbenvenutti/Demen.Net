namespace Demen.Application.Providers.ContentProvider.Dto;

public class ExternalVideoDto
{
	public required string YoutubeId { get; init; }
	public required string Title { get; init; }
	public required string Description { get; init; }
	public required string ThumbnailUrl { get; init; }
	public required string ChannelId { get; init; }
	public DateTime PublishedAt { get; init; }
}
