using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Video.Commands.CreateVideo;
using Demen.Application.CQRS.Video.Queries.FindVideo;
using Demen.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demen.API.Controllers;

[ApiController]
[Route(template: "api/v1/videos")]
[Produces(contentType: "application/json")]

public class VideoController : ControllerBase
{
	private readonly IMediator _mediator;

	public VideoController(IMediator mediator)
	{
		_mediator = mediator;
	}

	// ---- routes ------------------------------------------------------ //

	[HttpPost]
	[ProducesResponseType(
		statusCode: StatusCodes.Status201Created,
		type: typeof(Response<>)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status400BadRequest,
		type: typeof(Response<>)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status500InternalServerError,
		type: typeof(Response<EmptyDto>)
	)]

	public async Task<IActionResult> CreateVideo(CreateVideoRequest request)
	{

		var response = await _mediator.Send(request);

		if (!response.IsSuccess)
			return StatusCode(
				statusCode: response.HttpStatusCode,
				value: response
			);

		return CreatedAtAction(
			actionName: nameof(GetVideoById),
			routeValues: new { id = response.Data!.Id },
			value: response
		);
	}

	[HttpGet(template: "{id:Guid}")]
	public async Task<IActionResult> GetVideoById(
		[FromRoute] Guid id,
		[FromQuery] bool channel = false,
		[FromQuery] bool manager = false
	)
	{
		var request = new FindVideoRequest
		{
			Id = id,
			IncludeChannel = channel,
			IncludeManager = manager
		};

		var result = await _mediator.Send(request);

		return StatusCode(
			statusCode: result.HttpStatusCode,
			value: result
		);
	}
}
