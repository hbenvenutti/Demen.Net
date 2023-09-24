using Demen.Data.Contexts;
using Demen.Data.Entities;
using Demen.Domain.Content.Channel;

namespace Demen.Data.Repositories;

public class ChannelRepository : IChannelRepository
{
	private readonly IDemenContext _dbContext;

	public ChannelRepository(IDemenContext dbContext)
	{
		_dbContext = dbContext;
	}

	// ---- write ----------------------------------------------------------- //

	public async Task<ChannelDomain> CreateAsync(ChannelDomain domain)
	{
		var entity = (ChannelEntity)domain;

		await _dbContext.Channels.AddAsync(entity);

		return (ChannelDomain)entity!;
	}

	public async Task DeleteAsync(ChannelDomain domain)
	{
		throw new NotImplementedException();
	}

	// ---- read ------------------------------------------------------------ //

	public async Task<ChannelDomain?> FindByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<ChannelDomain?> GetByYoutubeIdAsync(string youtubeId)
	{
		throw new NotImplementedException();
	}
}
