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

	public int ManagerId { get; set; }
	public ManagerEntity? Manager { get; set; }

	// ---- operators ------------------------------------------------------- //

	public static implicit operator VideoEntity?(VideoDomain? domain)
	{
		if (domain is null)
			return null;

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
			Manager = null
		};
	}

	public static implicit operator VideoDomain?(VideoEntity? entity)
	{
		if (entity is null)
			return null;

		if(entity.Manager is not null)
			entity.Manager.Videos = null;

		return new VideoDomain(
			id: entity.Id,
			title: entity.Title,
			description: entity.Description,
			thumbnailUrl: entity.ThumbnailUrl,
			youtubeId: entity.YoutubeId,
			managerId: entity.ManagerId,
			externalId: entity.ExternalId,
			createdAt: entity.CreatedAt,
			updatedAt: entity.UpdatedAt,
			deletedAt: entity.DeletedAt,
			manager: entity.Manager,
			publishedAt: entity.PublishedAt
		);
	}
}
