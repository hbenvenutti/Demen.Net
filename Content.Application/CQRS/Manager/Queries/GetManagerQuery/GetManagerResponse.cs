using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;
using Ether.Outcomes;

namespace Demen.Application.CQRS.Manager.Queries.GetManagerQuery;

public class GetManagerResponse : IBaseCommandResponse<GetManagerResponseDto>
{
	public IOutcome<GetManagerResponseDto> Outcome { get; init; }

	// ---- constructors ---------------------------------------------------- //
	public GetManagerResponse(IOutcome<GetManagerResponseDto> outcome)
	{
		Outcome = outcome;
	}
}
