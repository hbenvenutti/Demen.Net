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
			ApplicationName = "Demen"
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
			.Parse(video.Snippet.PublishedAtRaw);

		return new ExternalVideoDto()
		{
			YoutubeId = video.Id,
			Title = video.Snippet.Title,
			Description = video.Snippet.Description,
			ThumbnailUrl = video.Snippet.Thumbnails.Default__.Url,
			PublishedAt = publishedAt,
		};
	}

	public async void FetchChannelInfo(string channelId)
	{
		var request = _youTubeService.Channels.List(part: "snippet");
		request.Id = channelId;

		var response = await request.ExecuteAsync();

		var channel = response.Items.FirstOrDefault();

		var publishedAt = DateTime.Parse(channel.Snippet.PublishedAtRaw);

		// return channel;
	}
}
