using Demen.Common.Enums;
using Demen.Domain.Base;
using Demen.Domain.Management.Manager;

namespace Demen.Domain.Content;

public class VideoDomain : BaseDomain
{
	// ---- properties ------------------------------------------------------ //

	public string Title { get; set; }
	public string Description { get; set; }
	public string ThumbnailUrl { get; set; }
	public string YoutubeId { get; set; }

	// ---- relationships --------------------------------------------------- //

	public int ManagerId { get; set; }
	public ManagerDomain? Manager { get; set; }

	// ---- constructors ---------------------------------------------------- //

	public VideoDomain(
		string title,
		string description,
		string thumbnailUrl,
		string youtubeId,
		DateTime createdAt,
		Guid externalId,
		int id = 0,
		int managerId = 0,
		Status status = Status.Active,
		ManagerDomain? manager = null,
		DateTime? updatedAt = null,
		DateTime? deletedAt = null
	)
	{
		Id = id;
		ExternalId = externalId;
		Status = status;
		CreatedAt = createdAt;
		UpdatedAt = updatedAt;
		DeletedAt = deletedAt;
		Title = title;
		Description = description;
		ThumbnailUrl = thumbnailUrl;
		YoutubeId = youtubeId;
		ManagerId = managerId;
		Manager = manager;
	}

	// ---- factories ------------------------------------------------------- //

	public static VideoDomain Create(
		string title,
		string description,
		string thumbnailUrl,
		string youtubeId,
		int managerId
	)
	{
		return new(
			title: title,
			description: description,
			thumbnailUrl: thumbnailUrl,
			youtubeId: youtubeId,
			createdAt: DateTime.UtcNow,
			externalId: Guid.NewGuid(),
			managerId: managerId
		);
	}
}
