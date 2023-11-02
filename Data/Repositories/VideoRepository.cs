using Demen.Data.Contexts;
using Demen.Data.Entities;
using Demen.Data.Helpers;
using Demen.Domain.Content.Video;
using Microsoft.EntityFrameworkCore;

namespace Demen.Data.Repositories;

public class VideoRepository : IVideoRepository
{
	private readonly IDemenContext _dbContext;

	public VideoRepository(IDemenContext dbContext)
	{
		_dbContext = dbContext;
	}

	// ---- write ----------------------------------------------------------- //

	public async Task<VideoDomain> CreateAsync(VideoDomain domain)
	{
		var entity = (VideoEntity)domain;

		await _dbContext.Videos.AddAsync(entity);

		await _dbContext.SaveChangesAsync();

		return (VideoDomain)entity;
	}

	public async Task DeleteAsync(VideoDomain domain)
	{
		var entity = (VideoEntity)domain;

		entity.DeleteEntity();

		_dbContext.Videos.Update(entity);

		await _dbContext.SaveChangesAsync();

		return;
	}

	// ---- read ------------------------------------------------------------ //

	public async Task<VideoDomain?> FindByIdAsync(
		Guid id,
		bool includeChannel = false,
		bool includeManager = false
	)
	{
		var query = _dbContext.Videos
			.AsNoTracking();

		if (includeChannel)
			query = query.Include(video => video.Channel);

		if (includeManager)
			query = query.Include(video => video.Manager);

		var video = await query
			.FirstOrDefaultAsync(video => video.ExternalId == id);

		if (video is null) return null;

		return (VideoDomain)video;
	}

	public async Task<VideoDomain?> FindByYoutubeIdAsync(string youtubeId)
	{
		var video = await _dbContext.Videos
			.AsNoTracking()
			.FirstOrDefaultAsync(video => video.YoutubeId == youtubeId);

		if (video is null) return null;

		return (VideoDomain)video;
	}

	public async Task<bool> ExistsByYoutubeIdAsync(string youtubeId)
	{
		return await _dbContext.Videos
			.AsNoTracking()
			.AnyAsync(video => video.YoutubeId == youtubeId);
	}
}
