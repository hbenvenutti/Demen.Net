using System.Diagnostics.CodeAnalysis;
using Demen.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Application.Error;
using Demen.Application.Helpers;
using Demen.Application.Utils;
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
	private readonly IUnityOfWork _unityOfWork;
	private readonly OutcomeErrorHelper<CreateManagerResponseDto>
		_outcomeErrorHelper;

	// ---- constructors ---------------------------------------------------- //
	public CreateManagerCommandHandler(
		IManagerRepository managerRepository,
		IEmailRepository emailRepository,
		IUnityOfWork unityOfWork
	)
	{
		_managerRepository = managerRepository;
		_emailRepository = emailRepository;
		_unityOfWork = unityOfWork;

		_outcomeErrorHelper =
			new OutcomeErrorHelper<CreateManagerResponseDto>();
	}

	// ---- methods --------------------------------------------------------- //
	public async Task<CreateManagerResponse> Handle(
		CreateManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		// ---- validation -------------------------------------------------- //

		if (!IsRequestDtoValid(requestDto: request.RequestDto))
			return new CreateManagerResponse(_outcomeErrorHelper
				.CreateOutcomeFailure(new InvalidDataError())
			);

		var storedEmail = await _emailRepository
			.FindByAddressAsync(request.RequestDto.Email);

		if (storedEmail is not null)
			return new CreateManagerResponse(_outcomeErrorHelper
				.CreateOutcomeFailure(new EmailInUseError())
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

		await _unityOfWork
			.CommitAsync(cancellationToken);

		// ---- response ---------------------------------------------------- //

		var responseDto = (CreateManagerResponseDto)manager;

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
