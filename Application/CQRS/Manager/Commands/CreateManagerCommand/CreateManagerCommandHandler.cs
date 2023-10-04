using System.Net;
using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Application.Dto;
using Demen.Application.Error;
using Demen.Common.Enums;
using Demen.Common.Helpers;
using Demen.Domain.Management.Email;
using Demen.Domain.Management.Manager;
using MediatR;

namespace Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;

public class CreateManagerCommandHandler
	: IRequestHandler<CreateManagerRequest, Response<CreateManagerResponseDto>>
{
	// ---- fields ---------------------------------------------------------- //

	private readonly IManagerRepository _managerRepository;
	private readonly IEmailRepository _emailRepository;

	// ---- constructors ---------------------------------------------------- //

	public CreateManagerCommandHandler(
		IManagerRepository managerRepository,
		IEmailRepository emailRepository
	)
	{
		_managerRepository = managerRepository;
		_emailRepository = emailRepository;
	}

	// ---- methods --------------------------------------------------------- //
	public async Task<Response<CreateManagerResponseDto>> Handle(
		CreateManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		// ---- validation -------------------------------------------------- //
		var dataValidationResult =
			IsRequestDtoValid(requestDto: request);

		if (!dataValidationResult.succeded)
			return new Response<CreateManagerResponseDto>(
				httpStatusCode: HttpStatusCode.BadRequest,
				statusCode: StatusCode.InvalidData,
				errorDto: new ApplicationErrorDto(
					dataValidationResult.errorMessage
				)
			);

		var storedEmail = await _emailRepository
			.FindByAddressAsync(request.Email);

		if (storedEmail is not null)
			return new Response<CreateManagerResponseDto>(
				httpStatusCode: HttpStatusCode.Conflict,
				statusCode: StatusCode.Conflict,
				errorDto: new ApplicationErrorDto(
					EmailInUseError.Message
				)
			);

		// ---- persistence ------------------------------------------------- //

		var manager = await _managerRepository
			.CreateAsync((ManagerDomainDto) request);

		var email = EmailDomain.Create(
			managerId: manager.Id,
			address: request.Email,
			type: request.EmailType?.StringToEnum<EmailType>()
		);

		await _emailRepository.CreateAsync(email);

		// ---- response ---------------------------------------------------- //

		var responseDto = (CreateManagerResponseDto)manager;

		return new Response<CreateManagerResponseDto>(
			isSuccess: true,
			httpStatusCode: HttpStatusCode.Created,
			statusCode: StatusCode.Succeeded,
			data: responseDto
		);
	}

	// ---- helpers --------------------------------------------------------- //

	private static (bool succeded, string errorMessage) IsRequestDtoValid(
		CreateManagerRequest requestDto
	)
	{
		return (
			succeded:
				requestDto.EmailType is null ||
				requestDto.EmailType.IsEnum<EmailType>(),

			errorMessage: new InvalidDataError(
				property: nameof(requestDto.EmailType)
			).Message
		);
	}
}
