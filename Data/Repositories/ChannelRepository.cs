using Demen.Data.Contexts;
using Demen.Data.Entities;
using Demen.Domain.Content.Channel;
using Microsoft.EntityFrameworkCore;

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

		await _dbContext.SaveChangesAsync();

		return (ChannelDomain)entity;
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

	public async Task<ChannelDomain?> FindByYoutubeIdAsync(string youtubeId)
	{
		var channelEntity = await _dbContext.Channels
			.AsNoTracking()
			.FirstOrDefaultAsync(channel => channel.YoutubeId == youtubeId);

		if (channelEntity is null) return null;

		return (ChannelDomain)channelEntity;
	}
}
