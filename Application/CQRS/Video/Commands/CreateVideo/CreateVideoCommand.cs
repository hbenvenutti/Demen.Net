using System.Net;
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
	: IRequestHandler<CreateVideoRequest, CreateVideoResponse>
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

	public async Task<CreateVideoResponse> Handle(
		CreateVideoRequest request,
		CancellationToken cancellationToken
	)
	{
		// ? ---- external -------------------------------------------------- //

		var videoDto = await _contentProvider
			.FetchVideoInfo(request.RequestDto.YoutubeId);

		if (videoDto is null)
			return new CreateVideoResponse()
			{
				ResponseDto = new ResponseDto<CreateVideoResponseDto>(
					httpStatusCode: (int)HttpStatusCode.NotFound,
					statusCode: (int)StatusCode.ExternalResourceNotFound,

					errorDto: new ApplicationErrorDto(
						new ResourceNotFoundError(Resources.ExternalVideo)
							.Message
					)
				)
			};

		var channelDto = await _contentProvider
			.FetchChannelInfo(videoDto.ChannelId);

		if (channelDto is null)
			return new CreateVideoResponse()
			{
				ResponseDto = new ResponseDto<CreateVideoResponseDto>(
					httpStatusCode: (int)HttpStatusCode.NotFound,
					statusCode: (int)StatusCode.ExternalResourceNotFound,

					errorDto: new ApplicationErrorDto(
						new ResourceNotFoundError(Resources.ExternalChannel)
							.Message
					)
				)
			};

		// ? ---- internal -------------------------------------------------- //

		var channelDomain = await _channelRepository
			.CreateAsync(ChannelDomain.Create(
				youtubeId: channelDto.YoutubeId,
				name: channelDto.Name,
				description: channelDto.Description,
				thumbnailUrl: channelDto.ThumbnailUrl
			));

		var videoDomain = await _videoRepository
			.CreateAsync(VideoDomain.Create(
				youtubeId: videoDto.YoutubeId,
				title: videoDto.Title,
				description: videoDto.Description,
				thumbnailUrl: videoDto.ThumbnailUrl,
				publishedAt: videoDto.PublishedAt,
				channelId: channelDomain.Id,
				managerId: 0 // todo: implement authentication
			));

		// ------------------------------------------------------------------ //

		return new CreateVideoResponse()
		{
			ResponseDto = new ResponseDto<CreateVideoResponseDto>(
				isSuccess: true,
				httpStatusCode: (int)HttpStatusCode.Created,
				statusCode: (int)StatusCode.Succeeded,
				data: (CreateVideoResponseDto)videoDomain
			)
		};
	}
}
