using Ether.Outcomes;
using Demen.Content.Application.CQRS.Base;
using Demen.Content.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;

namespace Demen.Content.Application.CQRS.Manager.Queries.GetManagerQuery;

public class GetManagerResponse : IBaseCommandResponse<GetManagerResponseDto>
{
	public IOutcome<GetManagerResponseDto> Outcome { get; init; }

	// ---- constructors ---------------------------------------------------- //
	public GetManagerResponse(IOutcome<GetManagerResponseDto> outcome)
	{
		Outcome = outcome;
	}
}
