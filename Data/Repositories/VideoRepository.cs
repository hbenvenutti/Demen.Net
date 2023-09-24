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

		return (VideoDomain)entity!;
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

	public async Task<VideoDomain?> FindByIdAsync(Guid id)
	{
		return (VideoDomain?) await _dbContext.Videos
			.AsNoTracking()
			.FirstOrDefaultAsync(video => video.ExternalId == id);
	}

	public async Task<VideoDomain?> FindByYoutubeIdAsync(string youtubeId)
	{
		return (VideoDomain?) await _dbContext.Videos
			.AsNoTracking()
			.FirstOrDefaultAsync(video => video.YoutubeId == youtubeId);
	}

	public async Task<bool> ExistsByYoutubeIdAsync(string youtubeId)
	{
		return await _dbContext.Videos
			.AsNoTracking()
			.AnyAsync(video => video.YoutubeId == youtubeId);
	}
}
