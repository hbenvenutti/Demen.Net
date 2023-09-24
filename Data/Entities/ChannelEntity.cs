using Demen.Domain.Content.Channel;
using Demen.Domain.Content.Video;

namespace Demen.Data.Entities;

public class ChannelEntity : BaseEntity
{
	// ---- properties ------------------------------------------------------ //

	public required string YoutubeId { get; set; }
	public required string Name { get; set; }
	public required string ThumbnailUrl { get; set; }
	public required string? Description { get; set; }

	// ---- relations ------------------------------------------------------- //

	public required ICollection<VideoEntity>? Videos { get; set; }

	// ---- operators ------------------------------------------------------- //

	public static implicit operator ChannelDomain?(ChannelEntity? entity)
	{
		if (entity is null) return null;

		var videos = entity.Videos?
			.Any()
		    ?? false
			? entity.Videos
				.Select(video => (VideoDomain)video!)
				.ToList()
			: null;

		return new ChannelDomain()
		{
			Name = entity.Name,
			YoutubeId = entity.YoutubeId,
			ThumbnailUrl = entity.ThumbnailUrl,
			Description = entity.Description,
			CreatedAt = entity.CreatedAt,
			UpdatedAt = entity.UpdatedAt,
			DeletedAt = entity.DeletedAt,
			Status = entity.Status,
			ExternalId = entity.ExternalId,
			Id = entity.Id,
			Videos = videos
		};
	}

	// ---------------------------------------------------------------------- //

	public static implicit operator ChannelEntity(ChannelDomain domain) =>
		new()
		{
			Id = domain.Id,
			ExternalId = domain.ExternalId,
			Status = domain.Status,
			CreatedAt = domain.CreatedAt,
			UpdatedAt = domain.UpdatedAt,
			DeletedAt = domain.DeletedAt,
			Name = domain.Name,
			YoutubeId = domain.YoutubeId,
			ThumbnailUrl = domain.ThumbnailUrl,
			Description = domain.Description,
			Videos = null
		};
}
