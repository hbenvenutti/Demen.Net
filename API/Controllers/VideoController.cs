using Demen.Application.CQRS.Video.Commands.CreateVideo;
using Demen.Application.CQRS.Video.Commands.CreateVideo.Dto;
using Demen.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demen.API.Controllers
{
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
		    type: typeof(ResponseDto<>)
	    )]

	    [ProducesResponseType(
		    statusCode: StatusCodes.Status400BadRequest,
		    type: typeof(ResponseDto<>)
	    )]

	    [ProducesResponseType(
		    statusCode: StatusCodes.Status500InternalServerError,
		    type: typeof(ResponseDto<EmptyDto>)
	    )]

	    public async Task<IActionResult> CreateVideo(CreateVideoRequestDto requestDto)
	    {
		    var request = new CreateVideoRequest()
		    {
			    RequestDto = requestDto
		    };

		    var response = await _mediator.Send(request);

		    if (!response.ResponseDto.IsSuccess)
				return StatusCode(
					statusCode: response.ResponseDto.HttpStatusCode,
					value: response.ResponseDto
				);

		    return CreatedAtAction(
			    actionName: nameof(GetVideoById),
			    routeValues: new { id = response.ResponseDto.Data!.Id },
			    value: response.ResponseDto
		    );
	    }

	    [HttpGet(template: "{id:Guid}")]
	    public async Task<IActionResult> GetVideoById(Guid id)
	    {
		    return Ok();
	    }
    }
}
