using Demen.Application.CQRS.Base;
using Demen.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;

namespace Demen.Application.CQRS.Manager.Queries.GetManagerQuery;

public class GetManagerRequest
	: IBaseCommandRequest<GetManagerResponse, GetManagerRequestDto>
{
	public GetManagerRequestDto RequestDto { get; init; }

	// ---- constructors ---------------------------------------------------- //
	public GetManagerRequest(GetManagerRequestDto requestDto)
	{
		RequestDto = requestDto;
	}
}
