using Demen.Content.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using Demen.Content.Application.Error;
using Demen.Content.Application.Helpers;
using Demen.Content.Domain.Manager;
using Ether.Outcomes;
using MediatR;

namespace Demen.Content.Application.CQRS.Manager.Queries.GetManagerQuery;

public class GetManagerQueryHandler :
	IRequestHandler<GetManagerRequest, GetManagerResponse>
{
	// ---- fields ---------------------------------------------------------- //
	private readonly IManagerRepository _managerRepository;
	private readonly OutcomeErrorHelper<GetManagerResponseDto> _outcomeErrorHelper;

	// ---- constructors ---------------------------------------------------- //
	public GetManagerQueryHandler(IManagerRepository managerRepository)
	{
		_managerRepository = managerRepository;
		_outcomeErrorHelper = new OutcomeErrorHelper<GetManagerResponseDto>();
	}

	// ---- methods --------------------------------------------------------- //
	public async Task<GetManagerResponse> Handle(
		GetManagerRequest request,
		CancellationToken cancellationToken
	)
	{
		try
		{
			var managerDomain = await _managerRepository
				.FindByIdAsync(request.RequestDto.Id);

			if (managerDomain is null)
				return new GetManagerResponse(
					_outcomeErrorHelper
						.CreateOutcomeFailure(
							new ResourceNotFoundError(
								nameof(ManagerDomain).GetResource()
							)
						)
				);

			GetManagerResponseDto responseDto = managerDomain;

			return new GetManagerResponse(outcome: Outcomes
				.Success(responseDto)
			);
		}

		catch (Exception e)
		{
			return new GetManagerResponse(_outcomeErrorHelper
				.CreateOutcomeFailure(new UnexpectedError(e.Message))
			);
		}
	}
}
