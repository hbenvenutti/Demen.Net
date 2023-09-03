using MediatR;
using Microsoft.AspNetCore.Mvc;
using Demen.Content.API.Dto;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;

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
		statusCode: StatusCodes.Status201Created,
		type: typeof(CreateManagerResponseDto)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status400BadRequest,
		type: typeof(ErrorDto)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status500InternalServerError,
		type: typeof(ErrorDto)
	)]

	public async Task<IActionResult> CreateManager(
		[FromBody] CreateManagerRequestDto requestDto
	)
	{
		var request = new CreateManagerRequest(requestDto: requestDto);

		var response = await _mediator
			.Send(request: request);

		if (response.Outcome.Failure)
		{
			var errorDto = new ErrorDto(response.Outcome.Messages);

			return StatusCode(
				statusCode: response.Outcome.StatusCode
				    ?? StatusCodes.Status500InternalServerError,
				value: errorDto
			);
		}

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
