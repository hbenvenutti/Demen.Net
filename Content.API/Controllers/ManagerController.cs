using System.Net;
using Demen.Content.Application.Manager.Commands.CreateManagerCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Demen.Content.Application.Manager.Commands.CreateManagerCommand.Dto;

namespace Demen.Content.API.Controllers;

[ApiController]
[Route(template: "api/v1/managers")]
[Produces(contentType: "application/json")]

public class ManagerController : ControllerBase
{
	private readonly IMediator _mediator;

	// ---- constructors ---------------------------------------------------- //
	public ManagerController(IMediator mediator)
	{
		_mediator = mediator;
	}

	// ---- routes ---------------------------------------------------------- //

	[HttpPost]
	[Consumes(contentType: "application/json")]
	[ProducesResponseType(
		statusCode: (int)HttpStatusCode.Created,
		type: typeof(CreateManagerResponseDto)
	)]
	[ProducesResponseType(
		statusCode: (int)HttpStatusCode.BadRequest,
		type: typeof(CreateManagerResponseDto)
	)]
	[ProducesResponseType(
		statusCode: (int)HttpStatusCode.InternalServerError,
		type: typeof(CreateManagerResponseDto)
	)]

	public async Task<IActionResult> CreateManager(
		[FromBody] CreateManagerRequestDto requestDto
	)
	{
		var request = new CreateManagerRequest(requestDto: requestDto);

		var response = await _mediator
			.Send(request: request);

		return CreatedAtAction(
			actionName: nameof(GetManagerById),
			routeValues: new { id = response.Outcome.Value.Id },
			value: response.Outcome.Value
		);
	}

	[HttpGet(template: "{id:guid}")]

	public async Task<IActionResult> GetManagerById(Guid id)
	{
		// var responseDto = await _mediator.Send(new GetManagersRequestDto());
		// return Ok(responseDto);
		return Ok();
	}

}
