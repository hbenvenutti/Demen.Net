using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Application.CQRS.Manager.Commands.DeleteManager;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using Demen.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demen.API.Controllers;

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
		type: typeof(Response<CreateManagerResponseDto>)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status400BadRequest,
		type: typeof(Response<CreateManagerResponseDto>)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status500InternalServerError,
		type: typeof(Response<EmptyDto>)
	)]

	public async Task<IActionResult> CreateManager(
		[FromBody] CreateManagerRequest request
	)
	{
		var response = await _mediator
			.Send(request: request);

		if (!response.IsSuccess)
			return StatusCode(
				statusCode: response.HttpStatusCode,
				value: response
			);

		return CreatedAtAction(
			actionName: nameof(GetManagerById),
			routeValues: new { id = response.Data!.Id },
			value: response
		);
	}

	// ---------------------------------------------------------------------- //

	[HttpGet(template: "{id:guid}")]
	[Consumes(contentType: "application/json")]
	[ProducesResponseType(
		statusCode: StatusCodes.Status200OK,
		type: typeof(GetManagerResponseDto)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status400BadRequest,
		type: typeof(Response<GetManagerResponseDto>)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status404NotFound,
		type: typeof(Response<GetManagerResponseDto>)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status500InternalServerError,
		type: typeof(Response<EmptyDto>)
	)]

	public async Task<IActionResult> GetManagerById([FromRoute] Guid id)
	{
		var request = new GetManagerRequest() {Id = id 	};

		var response = await _mediator
			.Send(request: request);

		return StatusCode(
			statusCode: response.HttpStatusCode,
			value: response
		);
	}

	// ---------------------------------------------------------------------- //

	[HttpDelete(template: "{id:guid}")]
	[Consumes(contentType: "application/json")]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(
		statusCode: StatusCodes.Status404NotFound,
		type: typeof(Response<EmptyDto>)
	)]
	[ProducesResponseType(
		statusCode: StatusCodes.Status500InternalServerError,
		type: typeof(Response<EmptyDto>)
	)]

	public async Task<IActionResult> DeleteManagerById([FromRoute] Guid id)
	{
		var request = new DeleteManagerRequest() { Id = id };

		var response = await _mediator
			.Send(request: request);

		return StatusCode(
			statusCode: response.HttpStatusCode,
			value: response
		);
	}
}
