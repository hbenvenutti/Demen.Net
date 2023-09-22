namespace Demen.Data.Entities;

public class VideoEntity : BaseEntity
{
	// ---- properties ------------------------------------------------------ //

	public required string Title { get; set; }
	public required string Description { get; set; }
	public required string ThumbnailUrl { get; set; }
	public required string YoutubeId { get; set; }

	// ---- relationships --------------------------------------------------- //

	public int ManagerId { get; set; }
	public ManagerEntity? Manager { get; set; }

	// ---- operators ------------------------------------------------------- //
}
