namespace Demen.Application.Providers.ContentProvider.Dto;

public class ExternalChannelDto
{
	public required string YoutubeId { get; init; }
	public required string Name { get; init; }
	public required string ThumbnailUrl { get; init; }
	public required string? Description { get; init; }
}
