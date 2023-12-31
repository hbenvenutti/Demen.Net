using Demen.Domain.Base;

namespace Demen.Domain.Content.Video;

public interface IVideoRepository : IRepository<VideoDomain>
{
	Task<VideoDomain?> FindByYoutubeIdAsync(string youtubeId);
	Task<bool> ExistsByYoutubeIdAsync(string youtubeId);

	Task<VideoDomain?> FindByIdAsync(
		Guid id,
		bool includeChannel = false,
		bool includeManager = false
	);
}
