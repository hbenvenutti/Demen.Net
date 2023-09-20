using Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using Demen.Application.Error;
using Demen.Application.Helpers;
using Demen.Application.Structs;
using Demen.Domain.Management.Manager;
using Ether.Outcomes;
using MediatR;

namespace Demen.Application.CQRS.Manager.Queries.GetManagerQuery;

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
		var managerDomain = await _managerRepository
			.FindByIdAsync(request.RequestDto.Id);

		if (managerDomain is null)
			return new GetManagerResponse(
				_outcomeErrorHelper
					.CreateOutcomeFailure(
						new ResourceNotFoundError(Resources.Manager)
					)
			);

		GetManagerResponseDto responseDto = managerDomain;

		return new GetManagerResponse(outcome: Outcomes
			.Success(responseDto)
		);
	}
}
