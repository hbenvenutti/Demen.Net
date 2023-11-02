using Demen.Application.Providers.ContentProvider.Dto;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;

namespace Demen.Application.Providers.ContentProvider;

public class YoutubeProvider : IContentProvider
{
	private readonly YouTubeService _youTubeService;

	public YoutubeProvider(IConfiguration configuration)
	{
		_youTubeService = new YouTubeService(new BaseClientService.Initializer()
		{
			ApiKey = configuration["Youtube:ApiKey"],
			ApplicationName = configuration["Youtube:ApplicationName"]
		});
	}

	public async Task<ExternalVideoDto?> FetchVideoInfo(string videoId)
	{
		var request = _youTubeService.Videos.List(part: "snippet");
		request.Id = videoId;

		var response = await request.ExecuteAsync();

		var video = response.Items.FirstOrDefault();

		if (video is null) return null;

		var publishedAt = DateTime
			.Parse(video.Snippet.PublishedAtRaw)
			.ToUniversalTime();

		return new ExternalVideoDto()
		{
			YoutubeId = video.Id,
			Title = video.Snippet.Title,
			Description = video.Snippet.Description,
			ThumbnailUrl = video.Snippet.Thumbnails.Default__.Url,
			ChannelId = video.Snippet.ChannelId,
			PublishedAt = publishedAt,
		};
	}

	public async Task<ExternalChannelDto?> FetchChannelInfo(string channelId)
	{
		var request = _youTubeService.Channels.List(part: "snippet");
		request.Id = channelId;

		var response = await request.ExecuteAsync();

		var channel = response.Items.FirstOrDefault();

		if (channel is null) return null;

		return new ExternalChannelDto()
		{
			YoutubeId = channel.Id,
			Name = channel.Snippet.Title,
			ThumbnailUrl = channel.Snippet.Thumbnails.Default__.Url,
			Description = channel.Snippet.Description,
		};
	}
}
