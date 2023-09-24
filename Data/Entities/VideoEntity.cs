using Demen.Domain.Content.Video;

namespace Demen.Data.Entities;

public class VideoEntity : BaseEntity
{
	// ---- properties ------------------------------------------------------ //

	public required string Title { get; set; }
	public required string Description { get; set; }
	public required string ThumbnailUrl { get; set; }
	public required string YoutubeId { get; set; }
	public required DateTime PublishedAt { get; set; }

	// ---- relationships --------------------------------------------------- //

	public required int ManagerId { get; set; }
	public required int ChannelId { get; set; }
	public required ManagerEntity? Manager { get; set; }
	public required ChannelEntity? Channel { get; set; }

	// ---- operators ------------------------------------------------------- //

	public static implicit operator VideoEntity(VideoDomain domain)
	{

		return new VideoEntity()
		{
			Id = domain.Id,
			Title = domain.Title,
			Description = domain.Description,
			ThumbnailUrl = domain.ThumbnailUrl,
			YoutubeId = domain.YoutubeId,
			ManagerId = domain.ManagerId,
			ExternalId = domain.ExternalId,
			CreatedAt = domain.CreatedAt,
			UpdatedAt = domain.UpdatedAt,
			DeletedAt = domain.DeletedAt,
			PublishedAt = domain.PublishedAt,
			Manager = null,
			Status = domain.Status,
			ChannelId = domain.ChannelId,
			Channel = null
		};
	}

	public static implicit operator VideoDomain?(VideoEntity? entity)
	{
		if (entity is null)
			return null;

		if(entity.Manager is not null)
			entity.Manager.Videos = null;

		if (entity.Channel is not null)
			entity.Channel.Videos = null;

		return new VideoDomain()
		{
			Id = entity.Id,
			Title = entity.Title,
			Description = entity.Description,
			ThumbnailUrl = entity.ThumbnailUrl,
			YoutubeId = entity.YoutubeId,
			ManagerId = entity.ManagerId,
			ExternalId = entity.ExternalId,
			CreatedAt = entity.CreatedAt,
			UpdatedAt = entity.UpdatedAt,
			DeletedAt = entity.DeletedAt,
			Manager = entity.Manager,
			PublishedAt = entity.PublishedAt,
			Status = entity.Status,
			ChannelId = entity.ChannelId,
			Channel = entity.Channel
		};
	}
}
