using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Application.CQRS.Manager.Commands.DeleteManager;
using Demen.Application.CQRS.Manager.Commands.DeleteManager.Dto;
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
		type: typeof(CreateManagerResponseDto)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status400BadRequest,
		type: typeof(ResponseDto<CreateManagerResponseDto>)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status500InternalServerError,
		type: typeof(ResponseDto<EmptyDto>)
	)]

	public async Task<IActionResult> CreateManager(
		[FromBody] CreateManagerRequestDto requestDto
	)
	{
		var request = new CreateManagerRequest(requestDto: requestDto);

		var response = await _mediator
			.Send(request: request);

		if (!response.ResponseDto.IsSuccess)
			return StatusCode(
				statusCode: response.ResponseDto.HttpStatusCode,
				value: response.ResponseDto
			);

		return CreatedAtAction(
			actionName: nameof(GetManagerById),
			routeValues: new { id = response.ResponseDto.Data!.Id },
			value: response.ResponseDto
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
		type: typeof(ResponseDto<GetManagerResponseDto>)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status404NotFound,
		type: typeof(ResponseDto<GetManagerResponseDto>)
	)]

	[ProducesResponseType(
		statusCode: StatusCodes.Status500InternalServerError,
		type: typeof(ResponseDto<EmptyDto>)
	)]

	public async Task<IActionResult> GetManagerById([FromRoute] Guid id)
	{
		var requestDto = new GetManagerRequestDto(id: id);

		var request = new GetManagerRequest(requestDto);

		var response = await _mediator
			.Send(request: request);

		return StatusCode(
			statusCode: response.ResponseDto.HttpStatusCode,
			value: response.ResponseDto
		);
	}

	// ---------------------------------------------------------------------- //

	[HttpDelete(template: "{id:guid}")]
	[Consumes(contentType: "application/json")]
	[ProducesResponseType(statusCode: StatusCodes.Status200OK)]
	[ProducesResponseType(
		statusCode: StatusCodes.Status404NotFound,
		type: typeof(ResponseDto<EmptyDto>)
	)]
	[ProducesResponseType(
		statusCode: StatusCodes.Status500InternalServerError,
		type: typeof(ResponseDto<EmptyDto>)
	)]

	public async Task<IActionResult> DeleteManagerById([FromRoute] Guid id)
	{
		var requestDto = new DeleteManagerRequestDto()
		{
			Id = id
		};

		var request = new DeleteManagerRequest(requestDto);

		var response = await _mediator
			.Send(request: request);

		return StatusCode(
			statusCode: response.ResponseDto.HttpStatusCode,
			value: response.ResponseDto
		);
	}
}
