using Demen.Domain.Base;

namespace Demen.Domain.Content.Channel;

public interface IChannelRepository : IRepository<ChannelDomain>
{
	Task<ChannelDomain?> FindByIdAsync(Guid id);
	public Task<ChannelDomain?> FindByYoutubeIdAsync(string youtubeId);
}
