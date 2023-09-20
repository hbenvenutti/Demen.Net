using System.Diagnostics.CodeAnalysis;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Application.Error;
using Demen.Application.Helpers;
using Demen.Common.Enums;
using Demen.Common.Helpers;
using Demen.Domain.Management.Email;
using Demen.Domain.Management.Manager;
using Ether.Outcomes;
using MediatR;

namespace Demen.Application.CQRS.Manager.Commands.CreateManagerCommand;

[ExcludeFromCodeCoverage]

public class CreateManagerCommandHandler
	: IRequestHandler<CreateManagerRequest, CreateManagerResponse>
{
	// ---- fields ---------------------------------------------------------- //
	private readonly IManagerRepository _managerRepository;
	private readonly IEmailRepository _emailRepository;
	private readonly OutcomeErrorHelper<CreateManagerResponseDto>
		_outcomeErrorHelper;

	// ---- constructors ---------------------------------------------------- //
	public CreateManagerCommandHandler(
		IManagerRepository managerRepository,
		IEmailRepository emailRepository
	)
	{
		_managerRepository = managerRepository;
		_emailRepository = emailRepository;

		_outcomeErrorHelper =
			new OutcomeErrorHelper<CreateManagerResponseDto>();
	}

	// ---- methods --------------------------------------------------------- //
	public async Task<CreateManagerResponse> Handle(
		CreateManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		if (!IsRequestDtoValid(requestDto: request.RequestDto))
			return new CreateManagerResponse(_outcomeErrorHelper
				.CreateOutcomeFailure(new InvalidDataError())
			);

		var managerDomain = (ManagerDomain)request.RequestDto;

		var email = await _emailRepository
			.FindByAddressAsync(request.RequestDto.Email);

		if (email is not null)
			return new CreateManagerResponse(_outcomeErrorHelper
				.CreateOutcomeFailure(new EmailInUseError())
			);

		var responseDto = (CreateManagerResponseDto) await _managerRepository
			.CreateAsync(managerDomain);

		return new CreateManagerResponse(
			outcome: Outcomes
				.Success(responseDto)
		);
	}

	// ---- helpers --------------------------------------------------------- //

	private static bool IsRequestDtoValid(CreateManagerRequestDto requestDto)
	{
		return requestDto.EmailType is null
			|| requestDto.EmailType.IsEnum<EmailType>();
	}
}
