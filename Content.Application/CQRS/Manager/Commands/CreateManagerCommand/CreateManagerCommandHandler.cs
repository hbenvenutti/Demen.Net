using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Content.Application.Error;
using Demen.Content.Application.Helpers;
using Demen.Content.Domain.Email;
using Demen.Content.Domain.Manager;
using Ether.Outcomes;
using MediatR;

namespace Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand;

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
		var managerDomain = (ManagerDomain)request.RequestDto;

		var email = await _emailRepository
			.FindByAddressAsync(request.RequestDto.Email);

		if (email is not null)
			return new CreateManagerResponse(_outcomeErrorHelper
				.CreateOutcomeFailure(new EmailInUSeError())
			);

		var responseDto = (CreateManagerResponseDto) await _managerRepository
			.CreateAsync(managerDomain);

		return new CreateManagerResponse(
			outcome: Outcomes
				.Success(responseDto)
		);
	}
}
