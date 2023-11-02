using Demen.Common.Enums;
using Demen.Domain.Base;
using Demen.Domain.Content.Channel;
using Demen.Domain.Management.Manager;

namespace Demen.Domain.Content.Video;

public class VideoDomain : BaseDomain
{
	// ---- properties ------------------------------------------------------ //

	public required string Title { get; set; }
	public required string Description { get; set; }
	public required string ThumbnailUrl { get; set; }
	public required string YoutubeId { get; set; }
	public DateTime PublishedAt { get; set; }

	// ---- relationships --------------------------------------------------- //

	public required int ManagerId { get; set; }
	public required int ChannelId { get; set; }
	public required ManagerDomain? Manager { get; set; }
	public required ChannelDomain? Channel { get; set; }

	// ---- factories ------------------------------------------------------- //

	public static VideoDomain Create(
		string title,
		string description,
		string thumbnailUrl,
		string youtubeId,
		int managerId,
		DateTime publishedAt,
		int channelId
	) =>
		new()
		{
			Id = 0,
			ExternalId = Guid.Empty,
			Title = title,
			Description = description,
			ThumbnailUrl = thumbnailUrl,
			YoutubeId = youtubeId,
			PublishedAt = publishedAt,
			ManagerId = managerId,
			Manager = null,
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = null,
			DeletedAt = null,
			Status = Status.Active,
			ChannelId = channelId,
			Channel = null
		};
}
