using System.Diagnostics.CodeAnalysis;
using System.Net;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Application.Dto;
using Demen.Application.Error;
using Demen.Common.Enums;
using Demen.Common.Helpers;
using Demen.Domain.Management.Email;
using Demen.Domain.Management.Manager;
using MediatR;

namespace Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;

[ExcludeFromCodeCoverage]

public class CreateManagerCommandHandler
	: IRequestHandler<CreateManagerRequest, CreateManagerResponse>
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
	public async Task<CreateManagerResponse> Handle(
		CreateManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		// ---- validation -------------------------------------------------- //
		var dataValidationResult =
			IsRequestDtoValid(requestDto: request.RequestDto);

		if (!dataValidationResult.succeded)
			return new CreateManagerResponse(
				new ResponseDto<CreateManagerResponseDto>(
					httpStatusCode: (int)HttpStatusCode.BadRequest,
					statusCode: (int)StatusCode.InvalidData,
					errorDto: new ApplicationErrorDto(
						dataValidationResult.errorMessage
					),
					data: null
				)
			);

		var storedEmail = await _emailRepository
			.FindByAddressAsync(request.RequestDto.Email);

		if (storedEmail is not null)
			return new CreateManagerResponse(
				new ResponseDto<CreateManagerResponseDto>(
					httpStatusCode: (int)HttpStatusCode.BadRequest,
					statusCode: (int)StatusCode.Conflict,
					errorDto: new ApplicationErrorDto(
						EmailInUseError.Message
					),
					data: null
				)
			);

		// ---- persistence ------------------------------------------------- //

		var manager = await _managerRepository
			.CreateAsync((ManagerDomain) request.RequestDto);

		var email = EmailDomain.Create(
			managerId: manager.Id,
			address: request.RequestDto.Email,
			type: request.RequestDto.EmailType?.StringToEnum<EmailType>()
		);

		await _emailRepository.CreateAsync(email);

		// ---- response ---------------------------------------------------- //

		var responseDto = (CreateManagerResponseDto)manager;

		return new CreateManagerResponse(
			new ResponseDto<CreateManagerResponseDto>(
				isSuccess: true,
				httpStatusCode: (int)HttpStatusCode.Created,
				statusCode: (int)StatusCode.Succeeded,
				data: responseDto
			)
		);
	}

	// ---- helpers --------------------------------------------------------- //

	private static (bool succeded, string errorMessage) IsRequestDtoValid(
		CreateManagerRequestDto requestDto
	)
	{
		return (
			succeded:
				requestDto.EmailType is null ||
				requestDto.EmailType.IsEnum<EmailType>(),

			errorMessage: "EmailType is invalid."
		);
	}
}
