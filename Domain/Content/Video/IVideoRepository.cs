using Demen.Domain.Base;

namespace Demen.Domain.Content.Video;

public interface IVideoRepository : IRepository<VideoDomain>
{
	Task<VideoDomain?> FindByYoutubeIdAsync(string youtubeId);
}
