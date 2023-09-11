using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Content.Application.Helpers;
using Demen.Content.Application.Manager.Commands.CreateManagerCommand;
using Demen.Content.Domain.Manager;
using Ether.Outcomes;
using MediatR;

namespace Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand;

public class CreateManagerCommandHandler
	: IRequestHandler<CreateManagerRequest, CreateManagerResponse>
{
	// ---- fields ---------------------------------------------------------- //
	private readonly IManagerRepository _managerRepository;
	private readonly OutcomeErrorHelper<CreateManagerResponseDto>
		_outcomeErrorHelper;

	// ---- constructors ---------------------------------------------------- //
	public CreateManagerCommandHandler(IManagerRepository managerRepository)
	{
		_managerRepository = managerRepository;
		_outcomeErrorHelper = new OutcomeErrorHelper<CreateManagerResponseDto>();
	}

	// ---- methods --------------------------------------------------------- //
	public async Task<CreateManagerResponse> Handle(
		CreateManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		var managerDomain = ManagerDomain.Create(
			name: request.RequestDto.Name,
			surname: request.RequestDto.Surname,
			password: request.RequestDto.Password
		);

		CreateManagerResponseDto responseDto = await _managerRepository
			.CreateAsync(managerDomain);

		return new CreateManagerResponse(
			outcome: Outcomes
				.Success(responseDto)
		);
	}
}
