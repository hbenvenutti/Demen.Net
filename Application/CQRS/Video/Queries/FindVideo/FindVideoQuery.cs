using System.Net;
using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Video.Queries.FindVideo.Dto;
using Demen.Application.Dto;
using Demen.Application.Error;
using Demen.Common.Enums;
using Demen.Common.Structs;
using Demen.Domain.Content.Video;
using MediatR;

namespace Demen.Application.CQRS.Video.Queries.FindVideo;

public class FindVideoQuery
	: IRequestHandler<FindVideoRequest, Response<FindVideoResponseDto>>
{
	private readonly IVideoRepository _videoRepository;

	public FindVideoQuery(IVideoRepository videoRepository)
	{
		_videoRepository = videoRepository;
	}

	public async Task<Response<FindVideoResponseDto>> Handle(
		FindVideoRequest request,
		CancellationToken cancellationToken
	)
	{
		var video = await _videoRepository.FindByIdAsync(
			id: request.Id,
			includeChannel: request.IncludeChannel,
			includeManager: request.IncludeManager
		);

		if (video is null)
			return new Response<FindVideoResponseDto>(
				httpStatusCode: (int)HttpStatusCode.NotFound,
				statusCode: (int)StatusCode.ResourceNotFound,
				errorDto: new ApplicationErrorDto(
					new ResourceNotFoundError(Resources.Video)
						.Message
				)
			);

		return new Response<FindVideoResponseDto>(
			isSuccess: true,
			httpStatusCode: (int)HttpStatusCode.OK,
			statusCode: (int)StatusCode.Succeeded,
			errorDto: null,
			data: new FindVideoResponseDto
			{
				Video = video
			}
		);
	}
}
