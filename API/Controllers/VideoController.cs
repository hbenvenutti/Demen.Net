using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
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

	    public async Task<IActionResult> CreateVideo(string youtubeId)
	    {
		    return CreatedAtAction(
			    actionName: nameof(GetVideoById),
			    routeValues: new { id = 1 },
			    value: new { }
		    );
	    }

	    [HttpGet(template: "{id:Guid}")]
	    public async Task<IActionResult> GetVideoById(Guid id)
	    {
		    return Ok();
	    }
    }
}
