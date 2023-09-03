using Ether.Outcomes;
using MediatR;
using Demen.Content.Application.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Content.Domain.Manager;

namespace Demen.Content.Application.Manager.Commands.CreateManagerCommand;

public class CreateManagerCommandHandler
	: IRequestHandler<CreateManagerRequest, CreateManagerResponse>
{
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
