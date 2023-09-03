using Demen.Content.Application.CQRS.Base;
using Demen.Content.Application.CQRS.Manager.Queries.GetManagerQuery.Dto;

namespace Demen.Content.Application.CQRS.Manager.Queries.GetManagerQuery;

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
