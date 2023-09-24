using Demen.Domain.Base;

namespace Demen.Domain.Content.Channel;

public interface IChannelRepository : IRepository<ChannelDomain>
{
	public Task<ChannelDomain?> FindByYoutubeIdAsync(string youtubeId);
}
