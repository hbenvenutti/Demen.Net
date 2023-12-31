using System.Net;
using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Video.Commands.CreateVideo.Dto;
using Demen.Application.Dto;
using Demen.Application.Error;
using Demen.Application.Providers.ContentProvider;
using Demen.Common.Enums;
using Demen.Common.Structs;
using Demen.Domain.Content.Channel;
using Demen.Domain.Content.Video;
using MediatR;

namespace Demen.Application.CQRS.Video.Commands.CreateVideo;

public class CreateVideoCommand
	: IRequestHandler<CreateVideoRequest, Response<CreateVideoResponseDto>>
{
	private readonly IVideoRepository _videoRepository;
	private readonly IChannelRepository _channelRepository;
	private readonly IContentProvider _contentProvider;

	public CreateVideoCommand(
		IVideoRepository videoRepository,
		IChannelRepository channelRepository,
		IContentProvider contentProvider
	)
	{
		_videoRepository = videoRepository;
		_contentProvider = contentProvider;
		_channelRepository = channelRepository;
	}

	// ---------------------------------------------------------------------- //

	public async Task<Response<CreateVideoResponseDto>> Handle(
		CreateVideoRequest request,
		CancellationToken cancellationToken
	)
	{
		// ? ---- external -------------------------------------------------- //

		var videoExists = await _videoRepository
			.ExistsByYoutubeIdAsync(request.YoutubeId);

		if (videoExists)
			return new Response<CreateVideoResponseDto>(
				httpStatusCode: HttpStatusCode.Conflict,
				statusCode: StatusCode.Conflict,

				errorDto: new ApplicationErrorDto(
					new ResourceAlreadyExistsError(Resources.Video)
						.Message
				)
			);

		var videoDto = await _contentProvider
			.FetchVideoInfo(request.YoutubeId);

		if (videoDto is null)
			return new Response<CreateVideoResponseDto>(
				httpStatusCode: HttpStatusCode.NotFound,
				statusCode: StatusCode.ExternalResourceNotFound,

				errorDto: new ApplicationErrorDto(
					new ResourceNotFoundError(Resources.ExternalVideo)
						.Message
				)
			);

		// ? ---- channel --------------------------------------------------- //

		var channelDomain = await _channelRepository
			.FindByYoutubeIdAsync(videoDto.ChannelId);

		if (channelDomain is null)
		{
			var result = await HandleChannelCreation(videoDto.ChannelId);

			if (!result.isSuccess)
				return new Response<CreateVideoResponseDto>(
					httpStatusCode: HttpStatusCode.NotFound,
					statusCode: StatusCode.ExternalResourceNotFound,

					errorDto: new ApplicationErrorDto(
						new ResourceNotFoundError(Resources.ExternalChannel)
							.Message
					)
				);

			channelDomain = result.channel;
		}

		// ? ---- internal -------------------------------------------------- //

		var videoDomain = await _videoRepository
			.CreateAsync(VideoDomain.Create(
				youtubeId: videoDto.YoutubeId,
				title: videoDto.Title,
				description: videoDto.Description,
				thumbnailUrl: videoDto.ThumbnailUrl,
				publishedAt: videoDto.PublishedAt,
				channelId: channelDomain!.Id,
				managerId: 1 // todo: implement authentication
			));

		// ------------------------------------------------------------------ //

		return new Response<CreateVideoResponseDto>(
			isSuccess: true,
			httpStatusCode: HttpStatusCode.Created,
			statusCode: StatusCode.Succeeded,
			data: (CreateVideoResponseDto)videoDomain
		);
	}

	// ---------------------------------------------------------------------- //

	private async Task<(ChannelDomain? channel, bool isSuccess)>
		HandleChannelCreation(string channelId)
	{
		var channelDto = await _contentProvider
			.FetchChannelInfo(channelId);

		if (channelDto is null)
			return (channel: null, isSuccess: false);

		var channel = await _channelRepository
			.CreateAsync(ChannelDomain.Create(
				youtubeId: channelDto.YoutubeId,
				name: channelDto.Name,
				description: channelDto.Description,
				thumbnailUrl: channelDto.ThumbnailUrl
			));

		return (channel, isSuccess: true);
	}
}
