using Ether.Outcomes;
using MediatR;
using Demen.Content.Application.Error;
using Demen.Content.Application.Helpers;
using Demen.Content.Application.CQRS.Manager.Commands.CreateManagerCommand.Dto;
using Demen.Content.Application.Manager.Commands.CreateManagerCommand;
using Demen.Content.Domain.Manager;

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
		try
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

		catch (Exception e)
		{
			return new CreateManagerResponse(_outcomeErrorHelper
				.CreateOutcomeFailure(new UnexpectedError(e.Message))
			);
		}
	}
}
