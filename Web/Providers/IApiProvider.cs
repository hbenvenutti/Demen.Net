using Demen.Domain.Content.Video;

namespace Demen.Web.Providers;

public interface IApiProvider
{
	Task<VideoDomain?> GetVideoAsync(Guid id);
}
