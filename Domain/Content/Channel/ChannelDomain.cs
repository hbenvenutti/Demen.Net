using Demen.Common.Enums;
using Demen.Domain.Base;
using Demen.Domain.Content.Video;

namespace Demen.Domain.Content.Channel;

public class ChannelDomain : BaseDomain
{
	// ---- properties ------------------------------------------------------ //

	public required string YoutubeId { get; init; }
	public required string Name { get; init; }
	public required string ThumbnailUrl { get; init; }
	public required string? Description { get; init; }

	// ---- relations ------------------------------------------------------- //

	public required ICollection<VideoDomain>? Videos { get; init; }

	// ---- methods --------------------------------------------------------- //

	public static ChannelDomain Create(
		string youtubeId,
		string name,
		string thumbnail,
		string? description = null
	) => new()
		{
			Id = 0,
			ExternalId = Guid.Empty,
			YoutubeId = youtubeId,
			Name = name,
			ThumbnailUrl = thumbnail,
			Description = description,
			Status = Status.Active,
			Videos = null,
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = null,
			DeletedAt = null
		};
}
