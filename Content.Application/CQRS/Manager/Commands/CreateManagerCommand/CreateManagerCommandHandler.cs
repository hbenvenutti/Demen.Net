using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Content.Domain.Manager;
using Ether.Outcomes;
using MediatR;

namespace Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand;

public class CreateManagerCommandHandler
	: IRequestHandler<CreateManagerRequest, CreateManagerResponse>
{
	// ---- fields ---------------------------------------------------------- //
	private readonly IManagerRepository _managerRepository;


	// ---- constructors ---------------------------------------------------- //
	public CreateManagerCommandHandler(IManagerRepository managerRepository)
	{
		_managerRepository = managerRepository;
	}

	// ---- methods --------------------------------------------------------- //
	public async Task<CreateManagerResponse> Handle(
		CreateManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		var managerDomain = (ManagerDomain)request.RequestDto;

		CreateManagerResponseDto responseDto = await _managerRepository
			.CreateAsync(managerDomain);

		return new CreateManagerResponse(
			outcome: Outcomes
				.Success(responseDto)
		);
	}
}
